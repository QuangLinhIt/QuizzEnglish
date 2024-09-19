using AutoMapper;
using QE.Business.Model;
using QE.Entity.Entity.Abstract.Question.Inherit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Mapper
{
    public class FillinBlankQuestionProfile:Profile
    {
        public FillinBlankQuestionProfile()
        {
            CreateMap<FillinBlankQuestion, FillinBlankQuestionModel>();
        }
    }
}
