using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class EnemyRepository : BaseRepository<Enemy>, IEnemyRepository
{
    public EnemyRepository(AppDbContext context) : base(context)
    {

    }

    // public Task<Enemy> CounterAttack(int enemyId, int characterId)
    // {
    //     throw new NotImplementedException();
    // }

}