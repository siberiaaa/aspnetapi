using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]

public class EnemyController : ControllerBase
{
    private IEnemyService _service;

    public EnemyController(IEnemyService enemyService)
    {
        _service = enemyService;
    }

    /// <summary>
    /// Method to get a list of all enemies.
    /// </summary>
    /// <returns>IEnumerable of enemies</returns>
    /// <remarks>
    /// Doesn't need parameters.
    /// </remarks>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Enemy>>> Get()
    {

        var Enemies = await _service.GetAll();

        return Ok(Enemies);
    }

/// <summary>
/// Method to get an enemy.
/// </summary>
/// <param name="id"></param>
/// <returns>Enemy object</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Enemy>>> Get(int id)
    {
        var Enemy = await _service.GetById(id);

        return Ok(Enemy);
    }

/// <summary>
/// Method to create an enemy.
/// </summary>
/// <param name="enemy"></param>
/// <returns>Enemy object</returns>
    [HttpPost]
    public async Task<ActionResult<Enemy>> Post([FromBody] Enemy enemy)
    {
        try
        {
            Enemy createdEnemy = await _service.Create(enemy);

            return Ok(createdEnemy);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

/// <summary>
/// Method to update an enemy.
/// </summary>
/// <param name="id"></param>
/// <param name="enemy"></param>
/// <returns>Enemy object</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Enemy>> Put(int id, [FromBody] Enemy enemy)
    {
        try
        {
            Enemy updatedEnemy = await _service.Update(id, enemy);
            return updatedEnemy;
        }
        catch(Exception ex)
        {
           return BadRequest(ex.Message); 
        }
    }

/// <summary>
/// Method to delete an enemy.
/// </summary>
/// <param name="id"></param>
/// <returns>Confirmation string.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Enemy>> Delete(int id)
    {
        try
        {
            await _service.Delete(id);
            return Ok("Enemy deleted");
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}