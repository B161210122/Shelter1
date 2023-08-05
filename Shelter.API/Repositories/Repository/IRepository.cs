using Shelter.Domain.Entities;
using System.Linq.Expressions;

namespace Shelter.API.Repositories.Repository
{
    public interface IRepository<T> where T : Entity
    {
        T Get(Expression<Func<T, bool>> condition);
        T GetById(int Id);
        List<T> GetAll(Expression<Func<T, bool>> condition = null);
        void Delete(T entity);
        T Add(T entity);
        void Update(T entity);
        int SaveChanges();
    }
}
