using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CharacterRepository : BaseRepository<Character>, ICharacterRepository
{
    public CharacterRepository(AppDbContext context) : base(context)
    {

    }

    // public Task LevelUp(Character character)
    // {
    //     throw new NotImplementedException();
    // }
}