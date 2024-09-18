using QE.Entity.Entity.Abstract.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Entity.Entity
{
    public class QuestionQuizz
    {
        public int QuizzId { get; set; }
        public Quizz Quizz { get; set; } = null!;
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
    }
}
