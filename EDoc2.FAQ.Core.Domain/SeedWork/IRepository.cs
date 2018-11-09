using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Domain.SeedWork
{
    public interface IRepository<T, in TPrimaryKey> where T: Entity<TPrimaryKey>
    {
        IUnitOfWork UnitOfWork { get; }

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<T> FindAsync(TPrimaryKey key);

        Task<int> DeleteAsync(T entity);
    }
}
