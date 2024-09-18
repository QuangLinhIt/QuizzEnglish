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
    public class CompetitionRepository:Repository<Competition>,ICompetitionRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CompetitionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public override async Task<Competition?> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Competitions
                .Include(x => x.CompetitionQuizzes)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
