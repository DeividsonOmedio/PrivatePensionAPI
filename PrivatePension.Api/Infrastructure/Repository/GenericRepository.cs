using Domain.Interfaces.InterfacesRepositories;
using Domain.Notifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GenericRepository<T>(DbContext context) : IGenericRepository<T> where T : class
    {
        protected readonly DbContext _context = context;
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<Notifies> Add(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return Notifies.Success();
            }
            catch (Exception ex)
            {
                return Notifies.Error(ex.Message);
            }
        }

        public async Task<Notifies> Update(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
                return Notifies.Success();
            }
            catch (Exception ex)
            {
                return Notifies.Error(ex.Message);
            }
        }

        public async Task<Notifies> Delete(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return Notifies.Success();
            }
            catch (Exception ex)
            {
                return Notifies.Error(ex.Message);
            }
        }

        public async Task<T?> GetById(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<T>> GetAll()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }
    }
}
