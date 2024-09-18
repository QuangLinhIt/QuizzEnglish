using QE.Entity.Entity.Abstract.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Entity.Entity
{
    public class CompetitionQuizz
    {
        public int Id { get; set; }
        public int QuizzId { get; set; }
        public Quizz Quizz { get; set; } = null!;

        public int CompetitionId { get; set; }
        public Competition Competition { get; set; } = null!;

        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
    }
}
