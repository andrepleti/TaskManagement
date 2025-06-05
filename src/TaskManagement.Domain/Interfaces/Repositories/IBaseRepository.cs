namespace TaskManagement.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        TEntity GetObjectBy(int id);

        void Add(TEntity entity);

        void Delete(TEntity entity);

        void Commit();
    }
}