using QE.Entity.Entity;
using QE.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Model
{
    public partial class QuizzScoreModel
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int QuizzId { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
    }
}
