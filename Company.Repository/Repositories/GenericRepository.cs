using Company.Data.Context;
using Company.Data.Entities;
using Company.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Company.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly Company2DbContext _context;

        public GenericRepository(Company2DbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }


        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll()
        => _context.Set<T>().ToList();

        public T GetById(int id)
        => _context.Set<T>().Find(id);
        
        public T GetByIdWithNoTracking(int id)
        => _context.Set<T>().AsNoTracking().FirstOrDefault(e => e.Id == id);

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
