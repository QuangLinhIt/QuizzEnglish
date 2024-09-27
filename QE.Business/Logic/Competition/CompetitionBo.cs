using AutoMapper;
using QE.Business.Model;
using QE.Core.Enum;
using QE.DataAccess.Repository.Detail.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Logic.Competition
{
    public class CompetitionBo : ICompetitionBo
    {
        private readonly ICompetitionUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CompetitionBo(ICompetitionUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CompetitionModel> GetById(int id)
        {
            var competition = await _unitOfWork.Competition.GetByIdAsync(id);
            if (competition != null)
            {
                return _mapper.Map<CompetitionModel>(competition);
            }
            return null!;
        }

        public async Task<int> Create(CompetitionModel model)
        {
            //1:check PlayerId
            var existingUser = await _unitOfWork.AppUser.GetByUserIdAsync(model.Player1Id);
            if(existingUser == null)
            {
                return (int)ResponseEnumType.Fail;
            }
            //2: create Competition
            var competition = new QE.Entity.Entity.Competition()
            {
                Player1Id = model.Player1Id,
                CompetitionStatus = model.CompetitionStatus,
                StartTime = DateTime.Now,
            };
            await _unitOfWork.Competition.InsertAsync(competition);
            return (int)ResponseEnumType.Sucess;
        }
        public async Task<int> Update(CompetitionModel model)
        {
            //1:check user
            var existingUser1 = await _unitOfWork.AppUser.GetByUserIdAsync(model.Player1Id);
            if (existingUser1 == null)
            {
                return (int)ResponseEnumType.Fail;
            }
            if (model.Player2Id != null)
            {
                var existingUser2 = await _unitOfWork.AppUser.GetByUserIdAsync(model.Player2Id);
                if (existingUser2 == null)
                {
                    return (int)ResponseEnumType.Fail;
                }
            }
            
            //2: update competition
            var existingCompetition = await _unitOfWork.Competition.GetByIdAsync(model.Id);
            if (existingCompetition == null)
            {
                return (int)ResponseEnumType.Fail;
            }
            var competition = new QE.Entity.Entity.Competition()
            {
                Id = model.Id,
                Player1Id = model.Player1Id,
                Player2Id = model.Player2Id,
                CompetitionStatus = model.CompetitionStatus,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
            };
            await _unitOfWork.Competition.UpdateAsync(competition);
            return (int)ResponseEnumType.Sucess;
        }

        public async Task<int> Delete(int id)
        {
            var existingCompetition = await _unitOfWork.Competition.GetByIdAsync(id);
            if (existingCompetition == null)
            {
                return (int)ResponseEnumType.Fail;
            }
            await _unitOfWork.Competition.DeleteAsync(existingCompetition);
            return (int)ResponseEnumType.Sucess;
        }
    }
}
