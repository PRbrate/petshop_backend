using Microsoft.EntityFrameworkCore;
using PetShop.Core.Base.Interfaces;

namespace PetShop.Core.Base
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public RepositoryBase(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> Create(T entity)
        {
            var TCreated = await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;

        }

        public async Task<T> Delete(T entity)
        {
            var TDelete = _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;

        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
