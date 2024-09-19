using QE.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Model
{
    public partial class CompetitionQuizzModel
    {
        public int Id { get; set; }
        public int QuizzId { get; set; }
        public int CompetitionId { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
    }
}
