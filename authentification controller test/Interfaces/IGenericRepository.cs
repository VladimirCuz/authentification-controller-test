using System.Linq.Expressions;

namespace authentification_controller_test.Interfaces
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        Task<bool> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> UpdateBulkAsync(IEnumerable<T> entities);
        Task<bool> DeleteBulkAsync(IEnumerable<T> entities);
        Task<bool> CreateBulkAsync(IEnumerable<T> entities);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetById(int id);
        Task<List<T>> GetAllEntities();
        Task<bool> UpdateFieldsAsync(T entity, Action<T> updateAction);
    }
}
