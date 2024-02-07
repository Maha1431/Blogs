using AutoMapper;
using DataAccessLayer.Entities;
using CommonLayer.DataTransferObjects;

namespace ServiceLayer.AutoMapperProfile
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Blogs, AddBlogDTO>().ReverseMap();

            CreateMap<Blogs, UpdateBlogDTO>().ReverseMap();

            CreateMap<Blogs, GetBlogDetailsDTO>().ReverseMap();
        }
    }
}
