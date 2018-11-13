using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Domain.SeedWork
{
    public interface IRepository<T, in TPrimaryKey> where T: Entity<TPrimaryKey>
    {
        IUnitOfWork UnitOfWork { get; }

        T Add(T entity);

        void Delete(T entity);

        T Update(T entity);

        Task<T> FindAsync(TPrimaryKey key);
    }
}
