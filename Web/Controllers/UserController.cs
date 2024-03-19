//using Microsoft.AspNetCore.Authorization; este es el authorize de tal
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces.Services;
using Web.Helpers;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    public UserController(IUserService userService)
    {
        _service = userService;
    }
    
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        var token = await _service.Login(user.Username, user.Password);
        if (token == null || token == string.Empty)
        {
            return BadRequest(new { message = "Username or Password is incorrect" });
        }
        return Ok(token);
    }
    /// <summary>
    /// Method to create an user.
    /// </summary>
    /// <param name="user"></param>
    /// <returns>User object</returns>
    [HttpPost]
    public async Task<ActionResult<User>> Post([FromBody] User user)
    {
        try
        {
            User createdUser = await _service.Create(user);

            return Ok(createdUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    /// <summary>
    /// Method to update an user.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="user"></param>
    /// <returns>Enemy object</returns>
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<User>> Put(int id, [FromBody] User user)
    {
        try
        {
            User updatedUser = await _service.Update(id, user);
            return updatedUser;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}