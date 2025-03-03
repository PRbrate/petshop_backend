using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
            ConverteDateTimeToUtc(entity);
            var TCreated = await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;

        }

        public async Task<T> Delete(T entity)
        {
            ConverteDateTimeToUtc(entity);
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;

        }

        public IQueryable<T> GetAllAsync()
        {
            return  _dbSet.AsQueryable<T>();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> Update(T entity)
        {
            ConverteDateTimeToUtc(entity);
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public bool Detached(T entity)
        {
            _dbSet.Entry(entity).State = EntityState.Detached;
            return true;
        }

        private void ConverteDateTimeToUtc<T>(T entidade)
        {
            var propertyDateTime = typeof(T).GetProperties()
                .Where(prop => prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?));

            foreach (var property in propertyDateTime)
            {
                var valorAtual = property.GetValue(entidade) as DateTime?;

                if (valorAtual.HasValue)
                {
                    property.SetValue(entidade, valorAtual.Value.ToUniversalTime());
                }
            }
        }

    }
}
