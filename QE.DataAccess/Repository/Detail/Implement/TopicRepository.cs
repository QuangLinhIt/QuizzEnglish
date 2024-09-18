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
    public class TopicRepository : Repository<Topic>, ITopicRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public TopicRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        
        public override async Task<Topic?> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Topics.Include(vt => vt.VocabularyTopics).FirstOrDefaultAsync(t => t.Id == id);
        }

        public override async Task<Topic> UpdateAsync(Topic topic)
        {
            var existingTopic = await _applicationDbContext.Topics.Include(vt => vt.VocabularyTopics).FirstOrDefaultAsync(t => t.Id == topic.Id);
            if (existingTopic!=null){
                _applicationDbContext.Topics.Update(topic);
                if (existingTopic.VocabularyTopics != null)
                {
                    existingTopic.VocabularyTopics.Clear();
                }
                if (topic.VocabularyTopics != null)
                {
                    foreach (var vocabularyTopic in topic.VocabularyTopics)
                    {
                        vocabularyTopic.TopicId = topic.Id;
                        await _applicationDbContext.VocabularyTopics.AddAsync(vocabularyTopic);
                    }
                }
                await _applicationDbContext.SaveChangesAsync();
            }
            return topic;
        }

        public override async Task<bool> DeleteAsync (Topic topic)
        {
            var existingTopic = await _applicationDbContext.Topics.Include(vt => vt.VocabularyTopics).FirstOrDefaultAsync(t => t.Id == topic.Id);
            if (existingTopic != null)
            {
                //1: delete data in table VocabularyTopic
                if (existingTopic.VocabularyTopics != null)
                {
                    existingTopic.VocabularyTopics.Clear();
                    await _applicationDbContext.SaveChangesAsync();
                    //2: kiểm tra nếu Vocabulary k chứa 1 quan hệ nào thì xóa Vocabulary đó
                    foreach(var vocabularyTopic in existingTopic.VocabularyTopics)
                    {
                        var existingVocabularyRelationship = await _applicationDbContext.VocabularyTopics.FirstOrDefaultAsync(vt => vt.VocabularyId == vocabularyTopic.VocabularyId);
                        if (existingVocabularyRelationship == null)
                        {
                            var existingVocabulary = await _applicationDbContext.Vocabularies.FindAsync(vocabularyTopic.VocabularyId);
                            if (existingVocabulary != null)
                            {
                                _applicationDbContext.Vocabularies.Remove(existingVocabulary);
                            }
                        }
                    }
                    await _applicationDbContext.SaveChangesAsync();
                }
                //3: Delete Topic
                _applicationDbContext.Topics.Remove(topic);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
