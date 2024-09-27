using AutoMapper;
using QE.Business.Model;
using QE.Core.Enum;
using QE.DataAccess.Repository.Detail.Implement;
using QE.DataAccess.Repository.Detail.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Logic.CompetitionQuizz
{
    public class CompetitionQuizzBo : ICompetitionQuizzBo
    {
        private readonly ICompetitionUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CompetitionQuizzBo(ICompetitionUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CompetitionQuizzModel>> GetByCompetitionId(int competitionId)
        {
            var competitionQuizzes = await _unitOfWork.CompetitionQuizz.GetByCompetitionId(competitionId);
            if (competitionQuizzes != null)
            {
                return _mapper.Map<IEnumerable<CompetitionQuizzModel>>(competitionQuizzes);
            }
            return null!;
        }

        public async Task<int> Create(CompetitionQuizzModel model)
        {
            //1:Check Quizz and Competition
            var existingQuizz = await _unitOfWork.Quizz.GetByIdAsync(model.QuizzId);
            var existingCompetition = await _unitOfWork.Competition.GetByIdAsync(model.CompetitionId);
            if(existingQuizz==null || existingCompetition == null)
            {
                return (int)ResponseEnumType.Fail;
            }
            //2: create CompetitionQuizz
            var competitionQuizz = new QE.Entity.Entity.CompetitionQuizz()
            {
                QuizzId = model.QuizzId,
                CompetitionId = model.CompetitionId,
                Player1Score = model.Player1Score,
                Player2Score = model.Player2Score,
            };
            await _unitOfWork.CompetitionQuizz.InsertAsync(competitionQuizz);
            return (int)ResponseEnumType.Sucess;
        }
    }
}
