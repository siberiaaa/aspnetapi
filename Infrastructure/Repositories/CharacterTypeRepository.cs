using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CharacterTypeRepository : BaseRepository<CharacterType>, ICharacterTypeRepository
{
    public CharacterTypeRepository(AppDbContext context) : base(context)
    {

    } 
}