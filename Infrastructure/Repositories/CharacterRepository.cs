using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CharacterRepository : BaseRepository<Character>, ICharacterRepository
{
    public CharacterRepository(AppDbContext context) : base(context)
    {
        
    } 
    public override async Task<IEnumerable<Character>> GetAllAsync()
    {
        return await base.dbSet.Include(x => x.CharacterType).ToListAsync();
    }
}