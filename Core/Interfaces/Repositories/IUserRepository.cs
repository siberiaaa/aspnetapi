using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    ValueTask<User> Login(string username, string password); // Me di cuenta que el login es solo una consulta
    //Ya me di cuenta que no era solo una consulta ya hecha
}