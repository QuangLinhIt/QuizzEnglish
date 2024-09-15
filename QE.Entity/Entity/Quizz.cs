using QE.Entity.Entity.Abstract.Question;
using QE.Entity.Enum;
using QE.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Entity.Entity
{
    public class Quizz
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string CreatorId { get; set; } = null!;
        public AppUser Creator { get; set; } = null!;

        public QuizzStatus QuizzStatus { get; set; }
        public ICollection<Question> Questions { get; set; } = null!;

        public ICollection<QuizzScore> QuizzScores { get; set; } = null!;
       
    }
}
