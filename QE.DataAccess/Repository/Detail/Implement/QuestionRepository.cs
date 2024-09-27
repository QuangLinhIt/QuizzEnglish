using Microsoft.EntityFrameworkCore;
using QE.DataAccess.Context;
using QE.DataAccess.Repository.Common.Implement;
using QE.DataAccess.Repository.Detail.Interface;
using QE.Entity.Entity.Abstract.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.DataAccess.Repository.Detail.Implement
{
    public class QuestionRepository:Repository<Question>,IQuestionRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public QuestionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public override async Task<Question?> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Questions
                .Include(x => x.QuestionQuizzes)
                .ThenInclude(x => x.Quizz)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public virtual async Task<bool> DeleteQuestionsNotRelationship()
        {
            var questionNotRelationship = await _applicationDbContext.Questions
                .Where(q => !_applicationDbContext.QuestionQuizzes.Any(qq => qq.QuestionId == q.Id))
                .ToListAsync();
            if(questionNotRelationship!=null && questionNotRelationship.Any())
            {
                _applicationDbContext.Questions.RemoveRange(questionNotRelationship);
                await _applicationDbContext.SaveChangesAsync();
            }
            return true;
        }
    }
}
