using CommonLayer.DataTransferObjects;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class BlogRepository : GenericRepository<Blogs>, IBlogRepository
    {
        public BlogRepository(BlogApplicationContext context) : base(context)
        {
            this.context = context;
        }
        public async Task AddBlog(Blogs entity)
        {
            try
            {
                await Add(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<IEnumerable<Blogs>> GetAllBlogs()
        {
            try
            {
                return await GetAll();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<bool> DeleteBlog(int Id)
        {
            try
            {
                return await Delete(Id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<Blogs> GetBlog(int Id)
        {
            try
            {
                return await Get(Id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> FindBlogTitle(string Title)
        {
            try
            {
                await Find(x => x.Title == Title);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> FindUser(int Id)
        {
            try
            {
               var exists = await Find(x => x.UserId == Id);
                if (exists == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }   
        }

        public async Task<bool> UpdateBlog(int Id, Blogs entity)
        {
            try
            {
                var result = await Update(Id, entity);
                if (result == true)
                    return await Task.FromResult(true);
                else
                    throw new Exception();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
