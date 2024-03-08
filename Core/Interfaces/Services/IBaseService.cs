namespace Core.Interfaces.Services
{
    public interface IBaseService<Entity> where Entity : class
    {
        
        Task<Entity> GetById(int id);
        Task<IEnumerable<Entity>> GetAll();
        Task<Entity> Create(Entity newEntity);
        Task<Entity> Update(int entidadToBeUpdatedId, Entity newEntidadValues);
        Task Delete(int entityId);
    }
}