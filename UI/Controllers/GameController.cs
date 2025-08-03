using BLL.Interfaces;
using DAL.DB;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using UI.Models.Mapping;

namespace UI.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly MinesweeperDbContext _dbContext;

        public GameController(IGameService gameService, MinesweeperDbContext dbContext)
        {
            _gameService = gameService;
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        public IActionResult Play(Guid gameId)
        {
            var board = _gameService.GetBoard(gameId);
            if(board == null)
                return NotFound("Game not found");
            
            var viewModel = BoardMapper.ToViewModel(board);
            viewModel.GameId = gameId; 
            ViewBag.IsWin = _gameService.IsWin(gameId);
            ViewBag.IsLost = _gameService.IsLost(gameId);
            return View(viewModel);
        }

        [HttpPost]
        [Route("Game/OpenCell")]
        public IActionResult OpenCell(Guid gameId, int row, int col)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            _gameService.OpenCell(gameId, row, col);

            bool isWin = _gameService.IsWin(gameId);
            bool isLost = _gameService.IsLost(gameId);

            if (isWin || isLost)
            {
                var player = _dbContext.Players.OrderByDescending(p => p.Id).FirstOrDefault();

                var startTime = _gameService.GetStartTime(gameId);
                double durationSeconds = 0;

                durationSeconds = (DateTime.UtcNow - startTime).TotalSeconds;
                var result = new GameResult
                {
                    PlayerId = player.Id,
                    DurationSeconds = (int)durationSeconds,
                    Result = isWin ? "Win" : "Loss",
                    DatePlayed = DateTime.UtcNow,
                    Score = _gameService.CalculateScore(gameId, isWin, durationSeconds) // напиши функцію нижче
                };

                _dbContext.GameResults.Add(result);
                _dbContext.SaveChanges();

                return Ok(new { status = "ended", result = isWin ? "win" : "loss" });
            }

            return Ok(new { status = "continue" });
        }

        [HttpPost]
        [Route("Game/ToggleFlag")]
        public IActionResult ToggleFlag(Guid gameId, int row, int col)
        {
            _gameService.ToggleFlag(gameId, row, col);
            return Ok(new { status = "toggled" });
        }

        [HttpPost]
        public IActionResult StartGame(string playerName, int rows, int columns, int minesCount)
        {
            var player = new Player
            {
                Name = playerName,
                RegisteredAt = DateTime.UtcNow
            };

            _dbContext.Players.Add(player);
            _dbContext.SaveChanges();

            var gameId = _gameService.StartGame(rows, columns, minesCount);
            return RedirectToAction("Play", new { gameId });
        }
    }
}
