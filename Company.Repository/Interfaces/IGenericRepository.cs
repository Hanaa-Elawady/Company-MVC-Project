using Company.Data.Entities;

namespace Company.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        T GetByIdWithNoTracking(int id);

        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
