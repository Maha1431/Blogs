using CommonLayer.DataTransferObjects;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ICommentRepository : IRepository<BlogComments>
    {
        Task AddComment(BlogComments entity);
        Task<IEnumerable<BlogComments>> GetAllComments();
        Task<BlogComments> GetComment(int Id);
        Task<bool> DeleteComment(int Id);
        Task<bool> UpdateComment(int Id, BlogComments entity);
        Task<bool> FindUserAndBlog(int UserId, int BlogId);
    }
}
