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
     
        public virtual async Task<IEnumerable<QuizzScore>>? GetQuizzScoresByQuizz(int quizzId,string userId)
        {
            return await _applicationDbContext.QuizzScores.Where(x => x.QuizzId == quizzId && x.UserId == userId).ToListAsync();
        }

        public override async Task<QuizzScore> UpdateAsync (QuizzScore quizzScore)
        {
            // Không thực hiện gì
            return await Task.FromResult(quizzScore);
        }
        public override async Task<bool> DeleteAsync (QuizzScore quizzScore)
        {
            // Không thực hiện việc xóa
            return await Task.FromResult(true);
        }
    }
}
