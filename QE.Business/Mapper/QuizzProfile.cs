using AutoMapper;
using QE.Business.Model;
using QE.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Mapper
{
    public partial class QuizzProfile:Profile
    {
        public QuizzProfile()
        {
            CreateMap<Quizz, QuizzModel>();
        }
    }
}
