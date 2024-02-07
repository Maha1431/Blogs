using AutoMapper;
using CommonLayer.DataTransferObjects;
using DataAccessLayer.Entities;

namespace ServiceLayer.AutoMapperProfile
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<BlogComments, AddCommentDTO>().ReverseMap();

            CreateMap<BlogComments, UpdateCommentDTO>().ReverseMap();

            CreateMap<BlogComments, GetCommentDetailsDTO>().ReverseMap();
        }
    }
}
