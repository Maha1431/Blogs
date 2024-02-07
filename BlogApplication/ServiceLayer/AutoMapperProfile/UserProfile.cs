using AutoMapper;
using CommonLayer.DataTransferObjects;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace ServiceLayer.AutoMapperProfile
{
    public class UserProfile : Profile 
    {
        public UserProfile()
        {
            CreateMap<AddUserDTO, Users>()
              .ForMember(opt => opt.Password, src => src.MapFrom(x => BCrypt.Net.BCrypt.HashPassword(x.Password)));

            CreateMap<Users, AddUserDTO>();

            CreateMap<Users, GetUserDetailsDTO>()
                .ForMember(opt => opt.FullName, src => src.MapFrom(x => x.FirstName + " "  + x.LastName));

            CreateMap<UpdateUserDTO, Users>()
             .ForMember(opt => opt.Password, src => src.MapFrom(x => BCrypt.Net.BCrypt.HashPassword(x.Password)));
        }
    }
}
