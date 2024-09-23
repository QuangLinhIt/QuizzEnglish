﻿using QE.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Logic.QuestionQuizz
{
    public interface IQuestionBo
    {
        Task<int> CreateFillinBlankQuestion(FillinBlankQuestionModel model);
        Task<int> CreateMultipleChoiceQuestion(MultipleChoiceQuestionModel model);
        Task<int> UpdateFillinBlankQuestion(FillinBlankQuestionModel model);
        Task<int> UpdateMultipleChoiceQuestion(MultipleChoiceQuestionModel model);
        Task<int> DeleteFillinBlankQuestion(FillinBlankQuestionModel model);
        Task<int> DeleteMultipleChoiceQuestion(MultipleChoiceQuestionModel model);
    }
}
