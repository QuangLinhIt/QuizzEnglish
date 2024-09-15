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
        public string OptionB { get; set; } = null!;
        public string OptionA { get; set; } = null!;
        public string OptionC { get; set; } = null!;
        public string OptionD { get; set; } = null!;
        public QuestionOption CorrectOption { get; set; }
    }
}
