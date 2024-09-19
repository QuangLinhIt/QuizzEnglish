using QE.Entity.Entity;
using QE.Entity.Enum;
using QE.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Model
{
    public partial class CompetitionModel
    {
        public int Id { get; set; }
        public string Player1Id { get; set; } = null!;
        public string? Player2Id { get; set; }
        public CompetitionStatus CompetitionStatus { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public ICollection<CompetitionQuizz>? CompetitionQuizzes { get; set; }
    }
}
