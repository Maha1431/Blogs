using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogApplicationContext context;
        public UnitOfWork(BlogApplicationContext context)
        {
            this.context = context;
        }
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
