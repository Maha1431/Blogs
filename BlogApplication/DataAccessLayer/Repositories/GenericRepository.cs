using DataAccessLayer.BaseEntities;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataAccessLayer.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class, IBaseEntity
    {
        protected BlogApplicationContext context;
        protected DbSet<T> dbSet;
        public GenericRepository(BlogApplicationContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public async Task<bool> Add(T entity)
        {
            await context.AddAsync(entity);
            return true;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var result = await this.dbSet.ToListAsync();
            if (result == null)
                return null;
            else
                return result;
        }

        public async Task<bool> Delete(int Id)
        {
            var exist = await dbSet.Where(x => x.Id == Id).FirstOrDefaultAsync();                                     
            if (exist == null)           
               return false;
            else
               context.Remove(exist);
               return true;
        }

        public async Task<T> Get(int Id)
        {        
            try
            {          
                var exist = await dbSet.Where(x => x.Id == Id).FirstOrDefaultAsync();
                if (exist != null)
                    return await dbSet.FindAsync(Id);
                else
                    return null;                                   
            }
            catch (Exception)
            {
                throw;
            }   
        }

        public async Task<bool> Update(int Id,T entity)
        {
            var exist = await dbSet.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (exist == null)
            {
                return false;
            }
            else
            {
                dbSet.Update(entity);
                return await Task.FromResult(true);           
            }
        }

        public async Task<List<T>> Find(Expression<Func<T, bool>> predicate)
        {
                List<T> query = await this.dbSet.Where(predicate).ToListAsync();
                return query;         
        }
    }
}   
