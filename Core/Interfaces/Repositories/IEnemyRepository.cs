using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IEnemyRepository : IBaseRepository<Enemy>
{
    // Task<Enemy> CounterAttack(int enemyId, int characterId);
}