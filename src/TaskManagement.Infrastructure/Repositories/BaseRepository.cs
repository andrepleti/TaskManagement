using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Infrastructure.Context;

namespace TaskManagement.Infrastructure.Repositories
{
    public class BaseRepository<TEntity>(DataBaseContext db) : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DataBaseContext _db = db;

        public TEntity GetObjectBy(int id)
        {
            return _db.Set<TEntity>().Where(x => x.Id == id).FirstOrDefault(Activator.CreateInstance<TEntity>());
        }

        public void Add(TEntity entity)
        {
            _db.Entry(entity).State = EntityState.Added;
        }
        
        public void Delete(TEntity entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
        }

        public void Commit()
        {
            _db.SaveChanges();
        }
    }
}