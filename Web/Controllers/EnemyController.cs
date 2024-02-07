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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Enemy>>> Get()
    {

        var Enemies = await _service.GetAll();

        return Ok(Enemies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Enemy>>> Get(int id)
    {
        var Enemy = await _service.GetEnemyById(id);

        return Ok(Enemy);
    }

    [HttpPut]
    public async Task<ActionResult<Enemy>> Put([FromBody] Enemy enemy)
    {
        try
        {
            Enemy createdEnemy = await _service.CreateEnemy(enemy);

            return Ok(createdEnemy);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Enemy>> Delete(int id)
    {
        try
        {
            await _service.DeleteEnemy(id);
            return Ok("Enemy deleted");
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}