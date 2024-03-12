using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;


namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
    private ICharacterService _service;
    public CharacterController(ICharacterService characterService)
    {
        _service = characterService;
    }

/// <summary>
/// Method to get a list of all characters.
/// </summary>
/// <returns>IEnumerable of characters</returns>
/// <remarks>
/// Doesn't need parameters.
/// </remarks>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Character>>> Get()
    {

        var Characters = await _service.GetAll();

        return Ok(Characters);
    }

/// <summary>
/// Method to get an character.
/// </summary>
/// <param name="id"></param>
/// <returns>Character object</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Character>>> Get(int id)
    {
        var Character = await _service.GetById(id);

        return Ok(Character);
    }

/// <summary>
/// Method to create a character.
/// </summary>
/// <param name="character"></param>
/// <returns>Character object</returns>
    [HttpPost]
    public async Task<ActionResult<Character>> Post([FromBody] Character character)
    {
        try
        {
            var createdCharacter = await _service.Create(character);

            return Ok(createdCharacter);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

/// <summary>
/// Method to update a character.
/// </summary>
/// <param name="id"></param>
/// <param name="character"></param>
/// <returns>Character object</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Character>> Put(int id, [FromBody] Character character)
    {
        try
        {
            Character updatedCharacter = await _service.Update(id, character);
            return updatedCharacter;
        }
        catch(Exception ex)
        {
           return BadRequest(ex.Message); 
        }
    }

/// <summary>
/// Method to delete a character.
/// </summary>
/// <param name="id"></param>
/// <returns>Confirmation string.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Character>> Delete(int id)
    {
        try
        {
            await _service.Delete(id);
            return Ok("Character deleted");
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

/// <summary>
/// Method to attack an enemy.
/// </summary>
/// <param name="idCharacter"></param>
/// <param name="idEnemy"></param>
/// <returns>Attack result string.</returns>
/// <remarks> 
/// Enemies can counterattack.
/// </remarks>
    [HttpPost("AttackEnemy")]
    public async Task<ActionResult<string>> AttackEnemy(int idCharacter, int idEnemy)
    {
        try
        {
            string result = await _service.AttackEnemy(idCharacter, idEnemy);
            return Ok(result);
            //return Ok(new{result}); tonto json 
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

/// <summary>
/// Method to heal a character.
/// </summary>
/// <param name="idCharacter"></param>
/// <returns>Character object.</returns>
/// <remarks> 
/// Heal is an ability to gain HP in exchange of MP.
/// </remarks>
    [HttpPost("Heal")]
    public async Task<ActionResult<Character>> Heal(int idCharacter)
    {
        try
        {
            var character = await _service.Heal(idCharacter);
            
            return Ok(character);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}