using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Web.Helpers;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class CharacterTypeController : ControllerBase
{
    private ICharacterTypeService _service;

    public CharacterTypeController(ICharacterTypeService characterTypeService)
    {
        _service = characterTypeService;
    }

    /// <summary>
    /// Method to get a list of all character types.
    /// </summary>
    /// <returns>IEnumerable of character types</returns>
    /// <remarks>
    /// Doesn't need parameters.
    /// </remarks>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CharacterType>>> Get()
    {

        var CharacterTypes = await _service.GetAll();

        return Ok(CharacterTypes);
    }

/// <summary>
/// Method to get a character types.
/// </summary>
/// <param name="id"></param>
/// <returns>CharacterType object</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<CharacterType>>> Get(int id)
    {
        var CharacterType = await _service.GetById(id);

        return Ok(CharacterType);
    }

/// <summary>
/// Method to create a character type.
/// </summary>
/// <param name="characterType"></param>
/// <returns>Character type object</returns>
    [HttpPost]
    public async Task<ActionResult<CharacterType>> Post([FromBody] CharacterType characterType)
    {
        try
        {
            CharacterType createdCharacterType = await _service.Create(characterType);

            return Ok(createdCharacterType);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

/// <summary>
/// Method to update a character type.
/// </summary>
/// <param name="id"></param>
/// <param name="characterType"></param>
/// <returns>CharacterType object</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<CharacterType>> Put(int id, [FromBody] CharacterType characterType)
    {
        try
        {
            CharacterType updatedCharacterType = await _service.Update(id, characterType);
            return updatedCharacterType;
        }
        catch(Exception ex)
        {
           return BadRequest(ex.Message); 
        }
    }

/// <summary>
/// Method to delete a character type.
/// </summary>
/// <param name="id"></param>
/// <returns>Confirmation string.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<CharacterType>> Delete(int id)
    {
        try
        {
            await _service.Delete(id);
            return Ok("Character type deleted");
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}