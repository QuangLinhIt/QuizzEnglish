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
    public class CompetitionProfile:Profile
    {
        public CompetitionProfile()
        {
            CreateMap<Competition, CompetitionModel>();
        }
    }
}
