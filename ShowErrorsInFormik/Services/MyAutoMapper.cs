using AutoMapper;
using ShowErrorsInFormik.Data.Identity;
using ShowErrorsInFormik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowErrorsInFormik.Services
{
    public class MyAutoMapper : Profile
    {
        public MyAutoMapper()
        {
            CreateMap<RegisterViewModel, AppUser>()
                .ForMember(x => x.Image, y => y.Ignore())
                .ForMember(x => x.UserName, y => y.MapFrom(x => x.Email));
        }
    }
}
