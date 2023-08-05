using Microsoft.EntityFrameworkCore;
using Shelter.Domain.Entities;
using System.Linq.Expressions;

namespace Shelter.API.Repositories.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        private readonly DbContext _context;
        public Repository(DbContext context)
        {
            _context = context;
        }

        public TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> condition)
        {
            return _context.Set<TEntity>().SingleOrDefault(condition);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> condition = null)
        {
            return condition == null
            ? _context.Set<TEntity>().ToList()
            : _context.Set<TEntity>().Where(condition).ToList();
        }

        public TEntity GetById(int Id)
        {
            return _context.Set<TEntity>().Find(Id);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
