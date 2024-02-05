using Core.Entities;

namespace Core.Interfaces.Services;

public interface ICharacterService
{
    Task<Character> GetCharacterById(int id);
    Task<IEnumerable<Character>> GetAll();
    Task<Character> CreateCharacter(Character newCharacter);
    Task<Character> UpdateCharacter(int characterToBeUpdatedId, Character newCharacterValues);
    Task DeleteCharacter(int personajeId);
    Task<Character> LevelUp(int characterId);
    Task<int> AttackEnemy(int characterId);
    Task<Character> Heal(int characterId);
    Task<int> TakeDamage(int characterId, int damage);
    Task<int> UpdateExp(int characterId, int exp);
    
}