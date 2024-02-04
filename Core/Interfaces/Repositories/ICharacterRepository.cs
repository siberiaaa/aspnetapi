using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface ICharacterRepository : IBaseRepository<Character>
{
    Task LevelUp(Character character);
}
