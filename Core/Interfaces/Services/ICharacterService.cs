using Core.Entities;
using Core.Enum;

namespace Core.Interfaces.Services;

public interface ICharacterService : IBaseService<Character>
{
    Task<Character> LevelUp(int characterId);
    Task<string> AttackEnemy(int characterId, int enemyId);
    Task<Character> Heal(int characterId);
    Task<IsAlive> TakeDamage(int characterId, int damage);
    Task<ItLeveledUp> UpdateExp(int characterId, int exp);
    
}