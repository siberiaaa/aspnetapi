using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
//lo de arriba para el token
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Services.Validators;
using System.Reflection;

namespace Services.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<User> Create(User newUser)
    {
        UserValidators validator = new();

        var validationResult = await validator.ValidateAsync(newUser);
        if (validationResult.IsValid)
        {
            await _unitOfWork.UserRepository.AddAsync(newUser);
            await _unitOfWork.CommitAsync();
        }
        else
        {
            throw new ArgumentException(validationResult.Errors.ToString());
        }

        return newUser;
    }

    public async Task<User> Update(int userToBeUpdatedId, User newUserValues)
    {
        UserValidators UserValidators = new();

        var validationResult = await UserValidators.ValidateAsync(newUserValues);
        if (!validationResult.IsValid)
            throw new ArgumentException(validationResult.Errors.ToString());

        User UserToBeUpdated = await _unitOfWork.UserRepository.GetByIdAsync(userToBeUpdatedId);

        if (UserToBeUpdated == null)
            throw new ArgumentException("Invalid User ID while updating");

        UserToBeUpdated.Username = newUserValues.Username;
        UserToBeUpdated.Password = newUserValues.Password;

        await _unitOfWork.CommitAsync();

        return await _unitOfWork.UserRepository.GetByIdAsync(userToBeUpdatedId);
    }

    public async Task<User> GetById(int id)
    {
        return await _unitOfWork.UserRepository.GetByIdAsync(id);
    }

    public async Task<string> Login(string username, string password)
    {
        User user = await _unitOfWork.UserRepository.Login(username, password);

        if (user == null)
        {
            return string.Empty;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("c2c3111663e00afe901d9c00ab169d36");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("id", user.ID.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        string userToken = tokenHandler.WriteToken(token);
        return userToken;
    }

    public string Logout(string jwt)
    {
        throw new NotImplementedException();
        //algo sobre invalidar el jwt que no sé
        //por ahora no
    }



    public string ValidateToken(string username, string jwt)
    {
        throw new NotImplementedException();
        //algo sobre validar el jwt que menos no sé
    }

    public Task Delete(int entityId)
    {
        throw new NotImplementedException();
        //No necesario
    }

    public Task<IEnumerable<User>> GetAll()
    {
        throw new NotImplementedException();
        //No necesario
    }
}