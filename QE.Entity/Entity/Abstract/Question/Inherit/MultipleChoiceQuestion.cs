using QE.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Entity.Entity.Abstract.Question.Inherit
{
    public class MultipleChoiceQuestion:Question
    {
        public required string OptionA { get; set; }
        public required string OptionB { get; set; }
        public required string OptionC { get; set; }
        public required string OptionD { get; set; }
        public QuestionOption CorrectOption { get; set; }
    }
}
