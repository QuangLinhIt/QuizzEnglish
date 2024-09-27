using AutoMapper;
using QE.Business.Model;
using QE.Core.Enum;
using QE.DataAccess.Repository.Detail.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Logic.QuizzScore
{
    public class QuizzScoreBo : IQuizzScoreBo
    {
        private readonly IQuestionQuizzUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public QuizzScoreBo(IQuestionQuizzUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<QuizzScoreModel>> GetQuizzScoreByQuizzId(int quizzId, string userId)
        {
            var quizzScores = await _unitOfWork.QuizzScore.GetQuizzScoreByUserIdAndQuizzId(quizzId, userId);
            if (quizzScores != null && quizzScores.Any())
            {
                return _mapper.Map<IEnumerable<QuizzScoreModel>>(quizzScores);
            }
            return null!;
        }

        public async Task<int> Create(QuizzScoreModel model)
        {
            //1:open unitOfWork
            await _unitOfWork.BeginTransactionAsync();
            //2:check existing quizz
            var existingQuizz = await _unitOfWork.Quizz.GetByIdAsync(model.QuizzId);
            if (existingQuizz == null)
            {
                await _unitOfWork.RollbackAsync();
                return (int)ResponseEnumType.Fail;
            }
            //3:add QuizzScore
            var quizzScore = new QE.Entity.Entity.QuizzScore()
            {
                Score = model.Score,
                QuizzId = model.QuizzId,
                UserId = model.UserId,
                CreatedAt = DateTime.Now,
            };
            await _unitOfWork.QuizzScore.InsertAsync(quizzScore);
            await _unitOfWork.SaveChangesAsync();
            //4: close unitOfWork
            await _unitOfWork.CommitAsync();
            return (int)ResponseEnumType.Sucess;
        }
    }
}
