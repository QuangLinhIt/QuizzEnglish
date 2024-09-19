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
    public partial class QuizzModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string CreatorId { get; set; } = null!;
        public QuizzStatus QuizzStatus { get; set; }
        public DateTime? LimitTime { get; set; }
        public ICollection<QuestionQuizz> QuestionQuizzes { get; set; } = null!;
    }
}
