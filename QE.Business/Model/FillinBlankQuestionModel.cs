using QE.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Model
{
    public partial class FillinBlankQuestionModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public QuestionType QuestionType { get; set; }

    }
}
