using Core.Entities;

namespace Core.Interfaces.Services;

public interface IUserService : IBaseService<User>
{
    Task<string> Login(string username, string password);
    string Logout(string jwt); //return task igual?
    string ValidateToken(string username, string jwt);
    // string CreateUser(User user);
    // string ModifyUser(int id, User user);

}