using DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IBlogRepository : IRepository<Blogs>
    {
        Task AddBlog(Blogs entity);
        Task<bool> UpdateBlog(int Id, Blogs entity);
        Task<bool> DeleteBlog(int Id);
        Task<IEnumerable<Blogs>> GetAllBlogs();
        Task<Blogs> GetBlog(int Id);
        Task<bool> FindBlogTitle(string Title);
        Task<bool> FindUser(int Id);
    }
}
