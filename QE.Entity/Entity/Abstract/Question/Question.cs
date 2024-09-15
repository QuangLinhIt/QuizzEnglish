using QE.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Entity.Entity.Abstract.Question
{
    public abstract class Question
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public QuestionType QuestionType { get; set; }
    }
}
