using QE.Entity.Enum;
using QE.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Entity.Entity
{
    public class Competition
    {
        public int Id { get; set; }

        public required string Player1Id { get; set; }
        public AppUser Player1 { get; set; } = null!;

        public required string Player2Id { get; set; }
        public AppUser Player2 { get; set; } = null!;

        public CompetitionStatus CompetitionStatus { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; } 
        public DateTime LimitTime { get; set; }

        public ICollection<CompetitionQuizz> CompetitionQuizzes { get; set; } = null!;
    }
}
