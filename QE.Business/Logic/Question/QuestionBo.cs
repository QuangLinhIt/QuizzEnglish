using AutoMapper;
using QE.Business.Model;
using QE.Core.Enum;
using QE.DataAccess.Repository.Detail.Interface;
using QE.Entity.Entity.Abstract.Question;
using QE.Entity.Entity.Abstract.Question.Inherit;
using QE.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Logic.Question
{
    public class QuestionBo : IQuestionBo
    {
        public readonly IQuestionQuizzUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public QuestionBo(IQuestionQuizzUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> CreateFillinBlankQuestion(FillinBlankQuestionModel model)
        {
            //1:open UnitOfWork
            await _unitOfWork.BeginTransactionAsync();
            //2: Add Question 
            var question = new QE.Entity.Entity.Abstract.Question.Inherit.FillinBlankQuestion()
            {
                Title = model.Title,
                Answer = model.Answer,
                QuestionType = model.QuestionType,
            };
            await _unitOfWork.Question.InsertAsync(question);
            await _unitOfWork.SaveChangesAsync();
            //2: Add QuestionQuizz relationship
            if (model.QuestionQuizzes == null || !model.QuestionQuizzes.Any())
            {
                await _unitOfWork.RollbackAsync();
                return (int)ResponseEnumType.Fail;
            }
            foreach (var item in model.QuestionQuizzes)
            {
                var questionQuizz = new QE.Entity.Entity.QuestionQuizz()
                {
                    QuizzId = item.QuizzId,
                    QuestionId = question.Id,
                };
                await _unitOfWork.QuestionQuizz.InsertAsync(questionQuizz);
                await _unitOfWork.SaveChangesAsync();
            }
            //3:close UnitOfWork
            await _unitOfWork.CommitAsync();
            return (int)ResponseEnumType.Sucess;
        }

        public async Task<int> CreateMultipleChoiceQuestion(MultipleChoiceQuestionModel model)
        {
            //1:Open UnitOfWork
            await _unitOfWork.BeginTransactionAsync();
            //2: Add Question 
            var question = new QE.Entity.Entity.Abstract.Question.Inherit.MultipleChoiceQuestion()
            {
                Title = model.Title,
                QuestionType = model.QuestionType,
                OptionA = model.OptionA,
                OptionB = model.OptionB,
                OptionC = model.OptionC,
                OptionD = model.OptionD,
                CorrectOption = model.CorrectOption,
            };
            await _unitOfWork.Question.InsertAsync(question);
            await _unitOfWork.SaveChangesAsync();
            //3: add QuestionQuizz relationship
            if(model.QuestionQuizzes==null || !model.QuestionQuizzes.Any())
            {
                await _unitOfWork.RollbackAsync();
                return (int)ResponseEnumType.Fail;
            }
            foreach(var item in model.QuestionQuizzes)
            {
                var questionQuizz = new QE.Entity.Entity.QuestionQuizz()
                {
                    QuizzId = item.QuizzId,
                    QuestionId = question.Id,
                };
                await _unitOfWork.QuestionQuizz.InsertAsync(questionQuizz);
                await _unitOfWork.SaveChangesAsync();
            }
            //4:close UnitOfWork
            await _unitOfWork.CommitAsync();
            return (int)ResponseEnumType.Sucess;
        }

        public async Task<int> UpdateFillinBlankQuestion(FillinBlankQuestionModel model)
        {
            //1:open UnitOfWork
            await _unitOfWork.BeginTransactionAsync();
            //2: find question
            var existingQuestion = await _unitOfWork.Question.GetByIdAsync(model.Id);
            if (existingQuestion == null)
            {
                await _unitOfWork.RollbackAsync();
                return (int)ResponseEnumType.Fail;
            }
            //3: update question
            var question = new QE.Entity.Entity.Abstract.Question.Inherit.FillinBlankQuestion()
            {
                Title = model.Title,
                Answer = model.Answer,
                QuestionType = model.QuestionType,
            };
            await _unitOfWork.Question.UpdateAsync(question);
            await _unitOfWork.SaveChangesAsync();
            //4: Delete all old QuestionQuizz relationship
            foreach(var item in existingQuestion.QuestionQuizzes)
            {
                var oldQuestionQuizz = new QE.Entity.Entity.QuestionQuizz()
                {
                    QuizzId = item.QuizzId,
                    QuestionId = model.Id,
                };
                await _unitOfWork.QuestionQuizz.DeleteAsync(oldQuestionQuizz);
                await _unitOfWork.SaveChangesAsync();
            } 
            //5: Add new QuestionQuizz 
            if(model.QuestionQuizzes==null || !model.QuestionQuizzes.Any())
            {
                await _unitOfWork.RollbackAsync();
                return (int)ResponseEnumType.Fail;
            }
            foreach(var item in model.QuestionQuizzes)
            {
                var newQuestionQuizz = new QE.Entity.Entity.QuestionQuizz()
                {
                    QuizzId = item.QuizzId,
                    QuestionId = model.Id,
                };
                await _unitOfWork.QuestionQuizz.InsertAsync(newQuestionQuizz);
                await _unitOfWork.SaveChangesAsync();
            }
            //6: close UnitOfWork
            await _unitOfWork.CommitAsync();
            return (int)ResponseEnumType.Sucess;
        }

        public async Task<int> UpdateMultipleChoiceQuestion(MultipleChoiceQuestionModel model)
        {
            //1: Open UnitOfWork
            await _unitOfWork.BeginTransactionAsync();
            //2: Find Question
            var existingQuestion = await _unitOfWork.Question.GetByIdAsync(model.Id);
            if (existingQuestion == null)
            {
                await _unitOfWork.RollbackAsync();
                return (int)ResponseEnumType.Fail;
            }
            //3: update question
            var question = new QE.Entity.Entity.Abstract.Question.Inherit.MultipleChoiceQuestion()
            {
                Title = model.Title,
                OptionA = model.OptionA,
                OptionB = model.OptionB,
                OptionC = model.OptionC,
                OptionD = model.OptionD,
                CorrectOption = model.CorrectOption,
                QuestionType = model.QuestionType,
            };
            await _unitOfWork.Question.UpdateAsync(question);
            await _unitOfWork.SaveChangesAsync();
            //4: Delete all old QuestionQuizz relationship
            foreach (var item in existingQuestion.QuestionQuizzes)
            {
                var oldQuestionQuizz = new QE.Entity.Entity.QuestionQuizz()
                {
                    QuizzId = item.QuizzId,
                    QuestionId = model.Id,
                };
                await _unitOfWork.QuestionQuizz.DeleteAsync(oldQuestionQuizz);
                await _unitOfWork.SaveChangesAsync();
            }
            //5: Add new QuestionQuizz 
            if (model.QuestionQuizzes == null || !model.QuestionQuizzes.Any())
            {
                await _unitOfWork.RollbackAsync();
                return (int)ResponseEnumType.Fail;
            }
            foreach (var item in model.QuestionQuizzes)
            {
                var newQuestionQuizz = new QE.Entity.Entity.QuestionQuizz()
                {
                    QuizzId = item.QuizzId,
                    QuestionId = model.Id,
                };
                await _unitOfWork.QuestionQuizz.InsertAsync(newQuestionQuizz);
                await _unitOfWork.SaveChangesAsync();
            }
            //6: close UnitOfWork
            await _unitOfWork.CommitAsync();
            return (int)ResponseEnumType.Sucess;

        }
      
        public async Task<int> DeleteQuestion(int id)
        {
            //1:open UnitOfWork
            await _unitOfWork.BeginTransactionAsync();
            //2: Find Question
            var existingQuestion = await _unitOfWork.Question.GetByIdAsync(id);
            if(existingQuestion == null)
            {
                await _unitOfWork.RollbackAsync();
                return (int)ResponseEnumType.Fail;
            }
            //3: delete QuestionQuizz relationship
            foreach(var item in existingQuestion.QuestionQuizzes)
            {
                var questionQuizz = new QE.Entity.Entity.QuestionQuizz()
                {
                    QuizzId = item.QuizzId,
                    QuestionId = item.QuestionId,
                };
                await _unitOfWork.QuestionQuizz.DeleteAsync(questionQuizz);
                await _unitOfWork.SaveChangesAsync();
            }
            //4: delete Question
            await _unitOfWork.Question.DeleteAsync(existingQuestion);
            await _unitOfWork.SaveChangesAsync();
            //5:close UnitOfWork
            await _unitOfWork.CommitAsync();
            return (int)ResponseEnumType.Sucess;
        }
    }
}
