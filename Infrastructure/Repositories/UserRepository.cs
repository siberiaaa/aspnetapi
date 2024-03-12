using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {

    }

    public virtual async ValueTask<User> Login(string username, string password)
    {   
        return await dbSet.Where(u => u.Username.Equals(username) && u.Password.Equals(password)).FirstOrDefaultAsync();
    }

}