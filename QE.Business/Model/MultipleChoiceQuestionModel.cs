using QE.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Model
{
    public partial class MultipleChoiceQuestionModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string OptionB { get; set; } = null!;
        public string OptionA { get; set; } = null!;
        public string OptionC { get; set; } = null!;
        public string OptionD { get; set; } = null!;
        public QuestionOption CorrectOption { get; set; }
        public QuestionType QuestionType { get; set; }
    }
}
