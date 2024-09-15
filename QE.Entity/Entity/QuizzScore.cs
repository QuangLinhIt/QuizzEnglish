using QE.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Entity.Entity
{
    public class QuizzScore
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int QuizzId { get; set; }
        public Quizz Quizz { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }
    }
}
