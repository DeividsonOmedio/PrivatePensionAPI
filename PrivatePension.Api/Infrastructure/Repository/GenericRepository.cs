﻿using Domain.Interfaces.InterfacesRepositories;
using Domain.Notifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
       public async Task<Notifies> Add(T entity)
        {
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

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
