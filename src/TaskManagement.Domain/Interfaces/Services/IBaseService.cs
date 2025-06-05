namespace TaskManagement.Domain.Interfaces.Services
{
    public interface IBaseService<TEntity>
    {
        void Add(TEntity task);

        void Delete(int id);
    }
}