using Microsoft.AspNetCore.Mvc;
using System;
using Domain.Minesweeper;
using BLL.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;

    public GamesController(IGameService gameService)
    {
        _gameService = gameService;
    }

    // POST api/Games
    [HttpPost]
    public ActionResult<Guid> StartGame([FromBody] StartGameRequest request)
    {
        try
        {
            var gameId = _gameService.StartGame(request.Rows, request.Columns, request.MinesCount);
            return Ok(gameId);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST api/Games/{id}/open
    [HttpPost("{id}/open")]
    public IActionResult OpenCell(Guid id, [FromBody] CellPosition pos)
    {
        try
        {
            bool success = _gameService.OpenCell(id, pos.Row, pos.Col);
            return Ok(success);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // POST api/Games/{id}/flag
    [HttpPost("{id}/flag")]
    public IActionResult ToggleFlag(Guid id, [FromBody] CellPosition pos)
    {
        try
        {
            _gameService.ToggleFlag(id, pos.Row, pos.Col);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // GET api/Games/{id} — get game state (board)
    [HttpGet("{id}")]
    public ActionResult<Board> GetBoard(Guid id)
    {
        try
        {
            var board = _gameService.GetBoard(id);
            return Ok(board);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // GET api/games/{id}/win
    [HttpGet("{id}/win")]
    public ActionResult<bool> IsWin(Guid id)
    {
        try
        {
            var result = _gameService.IsWin(id);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // GET api/Games/{id}/lost
    [HttpGet("{id}/lost")]
    public ActionResult<bool> IsLost(Guid id)
    {
        try
        {
            var result = _gameService.IsLost(id);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
}

/// <summary>
/// Just an additional model to get 
/// information from JSON format (Postman)
/// at "StartGame" method
/// </summary>

public class StartGameRequest
{
    public int Rows { get; set; }
    public int Columns { get; set; }
    public int MinesCount { get; set; }
}

public class CellPosition
{
    public int Row { get; set; }
    public int Col { get; set; }
}
