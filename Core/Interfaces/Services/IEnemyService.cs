using Core.Entities;
using Core.Enum;

namespace Core.Interfaces.Services;

public interface IEnemyService : IBaseService<Enemy>
{
    Task<int> CounterAttack(int enemyId);
    Task<IsAlive> TakeDamage(int enemyId, int damage);
    
}