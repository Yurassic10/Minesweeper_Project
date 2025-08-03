using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using Domain.Minesweeper;
using System.Threading.Tasks;
using DAL.Entities;
using System.ComponentModel.DataAnnotations;
using BLL.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class GameResultsController : ControllerBase
{
    private readonly IGameService _gameResultService;

    public GameResultsController(IGameService gameResultService)
    {
        _gameResultService = gameResultService;
    }

    // POST: api/GameResults
    [HttpPost]
    public async Task<IActionResult> AddGameResult([FromBody] CreateGameResultRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = new GameResult
        {
            PlayerId = request.PlayerId,
            DurationSeconds = request.DurationSeconds,
            Result = request.Result,
            DatePlayed = DateTime.UtcNow,
            Score = request.Score
        };

        await _gameResultService.AddGameResultAsync(result);
        return Ok("Game result added.");
    }

    // GET: api/GameResults/top/{count}
    [HttpGet("top/{count}")]
    public async Task<ActionResult<List<GameResult>>> GetTopResults(int count)
    {
        var results = await _gameResultService.GetTopResultsAsync(count);
        return Ok(results);
    }

    /// <summary>
    /// Just an additional model to get 
    /// information from JSON format (Postman)
    /// at "AddGameResult" method
    /// </summary>
    public class CreateGameResultRequest
    {
        [Required]
        public int PlayerId { get; set; }

        [Required]
        public int DurationSeconds { get; set; }

        [Required]
        public string Result { get; set; } = string.Empty;

        [Required]
        public int Score { get; set; }
    }

}
