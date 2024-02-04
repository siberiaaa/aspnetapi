using Core.Entities;
using Core.Interfaces.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Services;

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
    public async Task<ActionResult<IEnumerable<Character>>> Get(int id)
    {
        var Characters = await _service.GetCharacterById(id);

        return Ok(Characters);
    }
}