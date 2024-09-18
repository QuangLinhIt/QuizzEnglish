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
            return await _applicationDbContext.Quizzes.Include(x => x.QuestionQuizzes).FirstOrDefaultAsync(x => x.Id == id);
        }
        public override async Task<Quizz> UpdateAsync(Quizz quizz)
        {
            var existingQuizz = await _applicationDbContext.Quizzes
                .Include(x => x.QuestionQuizzes)
                .FirstOrDefaultAsync(x => x.Id == quizz.Id);
            if (existingQuizz != null)
            {
                _applicationDbContext.Quizzes.Update(quizz);
                if (existingQuizz.QuestionQuizzes != null)
                {
                    existingQuizz.QuestionQuizzes.Clear();
                }
                if (quizz.QuestionQuizzes != null)
                {
                    foreach(var questionQuizz in quizz.QuestionQuizzes)
                    {
                        questionQuizz.QuizzId = quizz.Id;
                        await _applicationDbContext.QuestionQuizzes.AddAsync(questionQuizz);
                    }
                }
                await _applicationDbContext.SaveChangesAsync();
            }
            return quizz;
        }

        public override async Task<bool> DeleteAsync (Quizz quizz)
        {
            var existingQuizz = await _applicationDbContext.Quizzes
                .Include(x => x.QuestionQuizzes)
                .FirstOrDefaultAsync(x => x.Id == quizz.Id);
            if(existingQuizz != null)
            {
                //1:delete data in table QuestionQuizz
                if (existingQuizz.QuestionQuizzes != null)
                {
                    existingQuizz.QuestionQuizzes.Clear();
                    await _applicationDbContext.SaveChangesAsync();
                    //2: kiểm tra nếu Question k chứa 1 quan hệ nào thì xóa Question đó
                    foreach(var questionQuizz in existingQuizz.QuestionQuizzes)
                    {
                        var existingQuestioRelationship = await _applicationDbContext.QuestionQuizzes
                            .FirstOrDefaultAsync(x => x.QuestionId == questionQuizz.QuestionId);
                        if (existingQuestioRelationship == null)
                        {
                            var existingQuestion = await _applicationDbContext.Questions
                                .FindAsync(questionQuizz.QuestionId);
                            if (existingQuestion != null)
                            {
                                _applicationDbContext.Questions.Remove(existingQuestion);
                            }
                        }
                    }
                    await _applicationDbContext.SaveChangesAsync();
                }
                //3:Delete Quizz
                _applicationDbContext.Quizzes.Remove(quizz);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
