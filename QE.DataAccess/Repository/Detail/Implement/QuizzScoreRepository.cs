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
    public class QuizzScoreRepository:Repository<QuizzScore>,IQuizzScoreRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public QuizzScoreRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        
        public virtual async Task<IEnumerable<QuizzScore>> GetQuizzScoreByUserIdAndQuizzId(int quizzId,string userId)
        {
            return await _applicationDbContext.QuizzScores
                .Where(x => x.QuizzId == quizzId && x.UserId == userId)
                .ToListAsync();
        }
         public async Task<bool> DeleteByQuizzId(int quizzId)
        {
            var quizzScores = await _applicationDbContext.QuizzScores.Where(x => x.QuizzId == quizzId).ToListAsync();
            if(quizzScores!=null && quizzScores.Any())
            {
                _applicationDbContext.QuizzScores.RemoveRange(quizzScores);
                await _applicationDbContext.SaveChangesAsync();
            }
            return true;
        }

    }
}
