﻿using QE.DataAccess.Repository.Common.Interface;
using QE.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.DataAccess.Repository.Detail.Interface
{
    public interface IQuizzScoreRepository:IRepository<QuizzScore>
    {
        Task<IEnumerable<QuizzScore>> GetQuizzScoreByUserIdAndQuizzId(int quizzId, string userId);
        Task<bool> DeleteByQuizzId(int quizzId);
    }
}
