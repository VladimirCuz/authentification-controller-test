using authentification_controller_test.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace authentification_controller_test
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DataContext _context;
        protected DbSet<T> _dbSet;
        public GenericRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<bool> CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateBulkAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteBulkAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> CreateBulkAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
        public async Task<T> GetById(int id)
            {
                var idProperty = typeof(T).GetProperties()
                                    .FirstOrDefault(prop => prop.Name.EndsWith("Id") && prop.PropertyType == typeof(int));

                if (idProperty == null)
                {
                    throw new InvalidOperationException($"Сущность {typeof(T).Name} не имеет индентификатора типа int");
                }
                if (id == 0)
                {
                    throw new InvalidOperationException("Значение id не может быть равно нулю.");
                }
                var allEntities = await _dbSet.ToListAsync();
                return allEntities.FirstOrDefault(e => (int)idProperty.GetValue(e) == id);
        }
        public async Task<List<T>> GetAllEntities()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<bool> UpdateFieldsAsync(T entity, Action<T> updateAction)
        {
                updateAction(entity);
                return await _context.SaveChangesAsync() > 0;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();

                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
