using Core.Entities;
using Core.Enum;

namespace Core.Interfaces.Services;

public interface IEnemyService
{
    Task<Enemy> GetEnemyById(int id);
    Task<IEnumerable<Enemy>> GetAll();
    Task<Enemy> CreateEnemy(Enemy newEnemy);
    Task<Enemy> UpdateEnemy(int enemyToBeUpdatedId, Enemy newEnemyValues);
    Task DeleteEnemy(int enemyId);
    Task<int> CounterAttack(int enemyId);
    Task<IsAlive> TakeDamage(int enemyId, int damage);
    
}