using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using Domain.Minesweeper;
using System.Threading.Tasks;
using DAL.Entities;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayersController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    // POST api/Players
    [HttpPost]
    public async Task<IActionResult> AddPlayer([FromBody] CreatePlayerRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var player = new Player { Name = request.Name };

        await _playerService.AddPlayerAsync(player);

        return Ok("Player created successfully.");
    }

    // GET api/Players/{name}
    [HttpGet("{name}")]
    public async Task<ActionResult<Player>> GetPlayerByName(string name)
    {
        var player = await _playerService.GetByNameAsync(name);

        if (player == null)
            return NotFound($"Player with name '{name}' not found.");

        return Ok(player);
    }
    public class CreatePlayerRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
