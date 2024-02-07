using Core.Entities;
using Core.Interfaces.Services;
using Infrastructure.Migrations;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Character>>> Get()
    {

        var Characters = await _service.GetAll();

        return Ok(Characters);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Character>>> Get(int id)
    {
        var Character = await _service.GetCharacterById(id);

        return Ok(Character);
    }

    [HttpPost]
    public async Task<ActionResult<Character>> Post([FromBody] Character character)
    {
        try
        {
            var createdCharacter = await _service.CreateCharacter(character);

            return Ok(createdCharacter);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<Character>> Put(int id, [FromBody] Character character)
    {
        try
        {
            Character updatedCharacter = await _service.UpdateCharacter(id, character);
            return updatedCharacter;
        }
        catch(Exception ex)
        {
           return BadRequest(ex.Message); 
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Character>> Delete(int id)
    {
        try
        {
            await _service.DeleteCharacter(id);
            return Ok("Character deleted");
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("AttackEnemy")]
    public async Task<ActionResult<Character>> AttackEnemy(int idCharacter, int idEnemy)
    {
        try
        {
            string result = await _service.AttackEnemy(idCharacter, idEnemy);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

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