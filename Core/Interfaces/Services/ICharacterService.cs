using Core.Entities;

namespace Core.Interfaces.Services;

public interface ICharacterService
{
    Task<Character> GetCharacterById(int id);
    Task<IEnumerable<Character>> GetAll();
    Task<Character> CreateCharacter(Character newCharacter);
    Task<Character> UpdateCharacter(int characterToBeUpdatedId, Character newCharacterValues);
    Task DeleteCharacter(int personajeId);
    Task<Character> LevelUp(int characterToBeUpdatedId, Character newCharacterValues);
}