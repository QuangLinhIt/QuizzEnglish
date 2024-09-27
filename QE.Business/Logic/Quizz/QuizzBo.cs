using AutoMapper;
using QE.Business.Model;
using QE.Core.Enum;
using QE.DataAccess.Repository.Detail.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Logic.Quizz
{
    public class QuizzBo : IQuizzBo
    {
        private readonly IQuestionQuizzUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public QuizzBo(IQuestionQuizzUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<QuizzModel>> GetAll(int pageIndex, int pageSize)
        {
            var quizzes = await _unitOfWork.Quizz.GetAsync(pageIndex, pageSize);
            if(quizzes!=null && quizzes.Any())
            {
                return _mapper.Map<IEnumerable<QuizzModel>>(quizzes);
            }
            return null!;
        }

        public async Task<QuizzModel> GetById(int id)
        {
            var quizz = await _unitOfWork.Quizz.GetByIdAsync(id);
            if (quizz != null)
            {
                return _mapper.Map<QuizzModel>(quizz);
            }
            return null!;
        }

        public async Task<int> Create(QuizzModel model)
        {
            //1: check User
            var existingUser = await _unitOfWork.AppUser.GetByUserIdAsync(model.CreatorId);
            if (existingUser == null)
            {
                return (int)ResponseEnumType.Fail;
            }
            //2: create quizz
            var quizz = new QE.Entity.Entity.Quizz()
            {
                Name = model.Name,
                CreatorId = model.CreatorId,
                QuizzStatus = model.QuizzStatus,
                LimitTime = model.LimitTime,
            };
            await _unitOfWork.Quizz.InsertAsync(quizz);
            return (int)ResponseEnumType.Sucess;
        }

        public async Task<int> Update(QuizzModel model)
        {
            //1:open unitOfWork
            await _unitOfWork.BeginTransactionAsync();
            //2: find quizz
            var existingQuizz = await _unitOfWork.Quizz.GetByIdAsync(model.Id);
            var existingUser = await _unitOfWork.AppUser.GetByUserIdAsync(model.CreatorId);
            if (existingQuizz == null || existingUser==null)
            {
                await _unitOfWork.RollbackAsync();
                return (int)ResponseEnumType.Fail;
            }
            //3: update quizz
            var quizz = new QE.Entity.Entity.Quizz()
            {
                Id = model.Id,
                Name = model.Name,
                CreatorId = model.CreatorId,
                QuizzStatus = model.QuizzStatus,
                LimitTime = model.LimitTime,
            };
            await _unitOfWork.Quizz.UpdateAsync(quizz);
            await _unitOfWork.SaveChangesAsync();
            //4: delete all old QuestionQuizz relationship
            if(existingQuizz.QuestionQuizzes!=null && existingQuizz.QuestionQuizzes.Any())
            {
                foreach(var item in existingQuizz.QuestionQuizzes)
                {
                    var oldQuestionQuizz = new QE.Entity.Entity.QuestionQuizz()
                    {
                        QuestionId = item.QuestionId,
                        QuizzId = model.Id,
                    };
                    await _unitOfWork.QuestionQuizz.DeleteAsync(oldQuestionQuizz);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            //5: add new list QuestionQuizz
            if(model.QuestionQuizzes!=null && model.QuestionQuizzes.Any())
            {
                foreach(var item in model.QuestionQuizzes)
                {
                    var newQuestionQuizz = new QE.Entity.Entity.QuestionQuizz()
                    {
                        QuestionId = item.QuestionId,
                        QuizzId = model.Id,
                    };
                    await _unitOfWork.QuestionQuizz.InsertAsync(newQuestionQuizz);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            //6: delete all question not realtionship
            await _unitOfWork.Question.DeleteQuestionsNotRelationship();
            await _unitOfWork.SaveChangesAsync();
            //7: close unitOfWork
            await _unitOfWork.CommitAsync();
            return (int)ResponseEnumType.Sucess;
        }

        public async Task<int> Delete(int id)
        {
            //1: open unitOfWork
            await _unitOfWork.BeginTransactionAsync();
            //2: find quizz
            var existingQuizz = await _unitOfWork.Quizz.GetByIdAsync(id);
            if (existingQuizz == null)
            {
                await _unitOfWork.RollbackAsync();
                return (int)ResponseEnumType.Fail;
            }
            //3: delete QuestionQuizz relationship
            if(existingQuizz.QuestionQuizzes!=null && existingQuizz.QuestionQuizzes.Any())
            {
                foreach(var item in existingQuizz.QuestionQuizzes)
                {
                    var questionQuizz = new QE.Entity.Entity.QuestionQuizz()
                    {
                        QuestionId = item.QuestionId,
                        QuizzId = existingQuizz.Id,
                    };
                    await _unitOfWork.QuestionQuizz.DeleteAsync(questionQuizz);
                }
            }
            //4: delete QuizzScore relationship
            await _unitOfWork.QuizzScore.DeleteByQuizzId(id);
            await _unitOfWork.SaveChangesAsync();
            //5: delete Quizz
            await _unitOfWork.Quizz.DeleteAsync(existingQuizz);
            await _unitOfWork.SaveChangesAsync();
            //6: close unit of work
            await _unitOfWork.CommitAsync();
            return (int)ResponseEnumType.Sucess;
        }
    }
}
