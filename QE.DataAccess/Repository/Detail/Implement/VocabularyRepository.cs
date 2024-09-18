using Microsoft.EntityFrameworkCore;
using QE.DataAccess.Context;
using QE.DataAccess.Repository.Common.Implement;
using QE.DataAccess.Repository.Detail.Interface;
using QE.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.DataAccess.Repository.Detail.Implement
{
    public class VocabularyRepository:Repository<Vocabulary>,IVocabularyRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public VocabularyRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        
        public override async Task<Vocabulary> InsertAsync(Vocabulary vocabulary)
        {
            //1: Add the vocabulary to generate the VocabularyId
            await _applicationDbContext.Vocabularies.AddAsync(vocabulary);
            await _applicationDbContext.SaveChangesAsync();

            //2: Set the VocabularyId for each VocabularyTopic and add them to the context
            foreach (var vocabularyTopic in vocabulary.VocabularyTopics)
            {
                vocabularyTopic.VocabularyId = vocabulary.Id; // Set the generated Vocabulary
                await _applicationDbContext.VocabularyTopics.AddAsync(vocabularyTopic);
            }

            //3: Save the VocabularyTopic relationships
            await _applicationDbContext.SaveChangesAsync();
            return vocabulary;
        }

        public override async Task<Vocabulary?> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Vocabularies.Include(x => x.VocabularyTopics).FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<IEnumerable<Vocabulary>>? GetVocabularyByTopic(int topicId)
        {
            return await _applicationDbContext.VocabularyTopics
                .Include(x=>x.Vocabulary)
                .Where(x => x.TopicId == topicId)
                .Select(x => x.Vocabulary)
                .ToListAsync();
        }

        public override async Task<Vocabulary> UpdateAsync(Vocabulary vocabulary)
        {
            var existingVocabulary = await _applicationDbContext.Vocabularies
                .Include(vt => vt.VocabularyTopics)
                .FirstOrDefaultAsync(v => v.Id == vocabulary.Id);
            if (existingVocabulary != null)
            {
                _applicationDbContext.Vocabularies.Update(vocabulary);
                existingVocabulary.VocabularyTopics.Clear();
                foreach(var vocabularyTopic in vocabulary.VocabularyTopics)
                {
                    vocabularyTopic.VocabularyId = vocabulary.Id;
                    await _applicationDbContext.VocabularyTopics.AddAsync(vocabularyTopic);
                }
                await _applicationDbContext.SaveChangesAsync();
            }
            return vocabulary;
        }

        public override async Task<bool> DeleteAsync(Vocabulary vocabulary)
        {
            var existingVocabulary = await _applicationDbContext.Vocabularies
               .Include(vt => vt.VocabularyTopics)
               .FirstOrDefaultAsync(v => v.Id == vocabulary.Id);
            if (existingVocabulary != null)
            {
                existingVocabulary.VocabularyTopics.Clear();
                _applicationDbContext.Vocabularies.Remove(vocabulary);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
