using DataAccessLayer.BaseEntities;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IRepository<T> where T : class, IBaseEntity
    {
        Task<bool> Add(T entity);
         Task<bool> Update(int Id,T entity);
        Task<bool> Delete(int Id);
        Task<T> Get(int Id);
        Task<IEnumerable<T>> GetAll();
        Task<List<T>> Find(Expression<Func<T, bool>> predicate);
    }
}
