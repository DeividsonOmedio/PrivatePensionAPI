using Domain.Entities;
using Domain.Interfaces.InterfacesRepositories;
using Domain.Notifications;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GenericRepository<T>(ContextBase context) : IGenericRepository<T> where T : class
    {
        protected readonly ContextBase _context = context;
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<Notifies> Add(T entity)
        {
            
            if (entity is Purchase purchase) //Para não adicionar novo produto e novo cliente
            {
                _context.Entry(purchase).State = EntityState.Added;

                if (purchase.Product != null)
                {
                    _context.Entry(purchase.Product).State = EntityState.Unchanged;
                }

                if (purchase.Client != null)
                {
                    _context.Entry(purchase.Client).State = EntityState.Unchanged;
                }
            }
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return Notifies.Success();
        }

        public async Task<Notifies> Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return Notifies.Success();
        }

        public async Task<Notifies> Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return Notifies.Success();
        }

        public async Task<T?> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
