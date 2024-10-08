﻿using QE.DataAccess.Repository.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.DataAccess.Repository.Detail.Interface
{
    public interface IQuestionQuizzUnitOfWork:IUnitOfWork
    {
        IQuestionRepository Question { get; }
        IQuizzRepository Quizz { get; }
        IQuestionQuizzRepository QuestionQuizz { get; }
        IQuizzScoreRepository QuizzScore { get; }
        IUserRepository AppUser { get; }
    }
}
