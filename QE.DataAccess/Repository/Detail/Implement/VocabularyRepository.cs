﻿using Microsoft.EntityFrameworkCore;
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
        
        public override async Task<Vocabulary?> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Vocabularies
                .Include(x => x.VocabularyTopics)
                .ThenInclude(x => x.Topic)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<bool> DeleteVocabulariesNotRelationship()
        {
            var vocabulariesNotRelationship = await _applicationDbContext.Vocabularies
                .Where(v => !_applicationDbContext.VocabularyTopics.Any(vt => vt.VocabularyId == v.Id))
                .ToListAsync();
            if(vocabulariesNotRelationship!=null && vocabulariesNotRelationship.Any())
            {
                _applicationDbContext.Vocabularies.RemoveRange(vocabulariesNotRelationship);
                await _applicationDbContext.SaveChangesAsync();
            }
            return true;
        }
    }
}
