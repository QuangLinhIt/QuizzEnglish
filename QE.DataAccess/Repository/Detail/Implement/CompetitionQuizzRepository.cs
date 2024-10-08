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
    public class CompetitionQuizzRepository:Repository<CompetitionQuizz>,ICompetitionQuizzRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CompetitionQuizzRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public virtual async Task<IEnumerable<CompetitionQuizz>> GetByCompetitionId(int competitionId)
        {
            return await _applicationDbContext.CompetitionQuizzes
                .Where(x => x.CompetitionId == competitionId)
                .ToListAsync();
        }
    }
}
