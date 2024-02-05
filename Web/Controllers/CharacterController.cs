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
    private IEnemyService _servicepormientras;
    public CharacterController(ICharacterService characterService, IEnemyService servicepormientras)
    {
        _service = characterService;
        _servicepormientras = servicepormientras;
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
            var character = await _service.GetCharacterById(idCharacter);
            var enemy = await _servicepormientras.GetEnemyById(idEnemy);
            if (character.Level < enemy.Level)
            {
                return Ok($"Your enemy is too strong, you can't fight it.");
            }



            int attackPoints = await _service.AttackEnemy(idCharacter);
            
            int damageTakedEnemy = await _servicepormientras.TakeDamage(idEnemy, attackPoints);

            if (damageTakedEnemy >= 0) //enemy mayor a eso sería reward ahora
            {
                int exp = await _service.UpdateExp(idCharacter, damageTakedEnemy);
                if (exp == 1) 
                {
                    return Ok($"You defeated your enemy. Your reward is {damageTakedEnemy} exp.\nYou leveled up!");
                }
                else
                {
                    return Ok($"You defeated your enemy. Your reward is {damageTakedEnemy} exp.");
                }             
            }

            int attackPointsEnemy = await _servicepormientras.CounterAttack(idEnemy);

            int damageTakedCharacter = await _service.TakeDamage(idCharacter, attackPointsEnemy);

            if (damageTakedCharacter == 1) //eso = 1 sería ded
            {
                return Ok($"Damage given: {attackPoints}\nDamage taken: {attackPointsEnemy}\nYou died.");     
            }

            return Ok($"Damage given: {attackPoints}\nDamage taken: {attackPointsEnemy}");

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