using AutoMapper;
using QE.Business.Model;
using QE.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Mapper
{
    public class AppUserProfile:Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUser, AppUserModel>()
                .ForMember(des => des.FullName, src => src.MapFrom(x => x.FirstName + " " + x.LastName));
        }
    }
}
