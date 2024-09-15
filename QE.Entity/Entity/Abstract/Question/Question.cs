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
        public required string Title { get; set; }
        public QuestionType QuestionType { get; set; }
    }
}
