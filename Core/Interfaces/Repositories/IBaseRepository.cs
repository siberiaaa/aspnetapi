namespace Core.Interfaces.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{

    ValueTask<TEntity> GetByIdAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync(); //Devuelve una coleccion del tipo de la entidad
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    Task Update(TEntity entity);
    Task AddAsync(TEntity entity);


}