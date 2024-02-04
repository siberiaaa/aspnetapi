using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    internal AppDbContext Context;

    internal DbSet<TEntity> dbSet;

    public BaseRepository(AppDbContext context)
    {
        this.Context = context;
        this.dbSet = context.Set<TEntity>();
    }

    public virtual async ValueTask<TEntity> GetByIdAsync(int id)
    {
        return await dbSet.FindAsync(id);
    }
    public virtual async ValueTask<TEntity> GetByIdAsync(int id, string nombre)
    {
        return await dbSet.FindAsync(id);
    }
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await dbSet.ToListAsync();
    }

    public virtual async void Remove(TEntity entity)
    {
        dbSet.Remove(entity);
    }
    public virtual async void RemoveRange(IEnumerable<TEntity> entities)
    {
        dbSet.RemoveRange(entities);
    }
    public virtual async Task Update(TEntity entityToUpdate)
    {
        dbSet.Attach(entityToUpdate);
        Context.Entry(entityToUpdate).State = EntityState.Modified;
    }
    public virtual async Task UpdateRange(IEnumerable<TEntity> entitiesToUpdate)
    {
        dbSet.AttachRange(entitiesToUpdate);
        Context.Entry(entitiesToUpdate).State = EntityState.Modified;
    }
    public virtual async Task AddAsync(TEntity entity)
    {
        await dbSet.AddAsync(entity);
    }
}
