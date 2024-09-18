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

        public override async Task<Question> InsertAsync(Question question)
        {
            //1: Add the question to generate the QuestionId
            await _applicationDbContext.Questions.AddAsync(question);
            await _applicationDbContext.SaveChangesAsync();
            //2: Add data in table QuestionQuizz
            foreach(var questionQuizz in question.QuestionQuizzes)
            {
                questionQuizz.QuestionId = question.Id;
                await _applicationDbContext.QuestionQuizzes.AddAsync(questionQuizz);
            }
            await _applicationDbContext.SaveChangesAsync();
            return question;
        }

        public override async Task<Question?> GetByIdAsync (int id)
        {
            return await _applicationDbContext.Questions.Include(x => x.QuestionQuizzes).FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<IEnumerable<Question>>? GetQuestionByQuizz(int quizzId)
        {
            return await _applicationDbContext.QuestionQuizzes
                .Include(x => x.Question)
                .Where(x => x.QuizzId == quizzId)
                .Select(x => x.Question)
                .ToListAsync();
        }
        public override async Task<Question> UpdateAsync(Question question)
        {
            var existingQuestion = await _applicationDbContext.Questions
                .Include(qq => qq.QuestionQuizzes)
                .FirstOrDefaultAsync(q => q.Id == question.Id);
            if (existingQuestion != null)
            {
                _applicationDbContext.Questions.Update(question);
                existingQuestion.QuestionQuizzes.Clear();
                foreach(var questionQuizz in question.QuestionQuizzes)
                {
                    questionQuizz.QuestionId = question.Id;
                    await _applicationDbContext.QuestionQuizzes.AddAsync(questionQuizz);
                }
                await _applicationDbContext.SaveChangesAsync();
            }
            return question;
        }

        public override async Task<bool> DeleteAsync(Question question)
        {
            var existingQuestion = await _applicationDbContext.Questions
                .Include(q => q.QuestionQuizzes)
                .FirstOrDefaultAsync(q => q.Id == question.Id);
            if (existingQuestion != null)
            {
                existingQuestion.QuestionQuizzes.Clear();
                _applicationDbContext.Questions.Remove(question);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
