using CommonLayer.DataTransferObjects;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class CommentRepository : GenericRepository<BlogComments>, ICommentRepository
    {
        public CommentRepository(BlogApplicationContext context) : base(context)
        {
            this.context = context;
        }

        public async Task AddComment(BlogComments entity)
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
        public async Task<IEnumerable<BlogComments>> GetAllComments()
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
        public async Task<BlogComments> GetComment(int Id)
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
        public async Task<bool> DeleteComment(int Id)
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

        public async Task<bool> FindUserAndBlog(int UserId,int BlogId)
        {
            var exists = await Find(x => x.UserId == UserId && x.BlogId == BlogId);
            if (exists == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> UpdateComment(int Id, BlogComments entity)
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

