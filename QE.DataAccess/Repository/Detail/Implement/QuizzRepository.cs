using Microsoft.EntityFrameworkCore;
using QE.DataAccess.Context;
using QE.DataAccess.Repository.Common.Implement;
using QE.DataAccess.Repository.Detail.Interface;
using QE.Entity.Entity;
using QE.Entity.Entity.Abstract.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.DataAccess.Repository.Detail.Implement
{
    public class QuizzRepository:Repository<Quizz>, IQuizzRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public QuizzRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public override async Task<Quizz?> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Quizzes
                .Include(x => x.QuestionQuizzes)
                .ThenInclude(x => x.Question)
                .FirstOrDefaultAsync(x => x.Id == id);
        } 
      
    }
}
