using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using UI.Models.ViewModels;

namespace UI.Controllers
{
    public class GameResultsController : Controller
    {
        private readonly IGameService _gameResultService;

        public GameResultsController(IGameService gameResultService)
        {
            _gameResultService = gameResultService;
        }

        public async Task<IActionResult> Index()
        {
            var results = await _gameResultService.GetAllAsync();
            var viewModels = results.Select(r => new GameResultsViewModel
            {
                Id = r.Id,
                PlayerId = r.PlayerId,
                PlayerName = r.Player?.Name ?? "Unknown",
                DurationSeconds = r.DurationSeconds,
                Result = r.Result,
                DatePlayed = r.DatePlayed,
                Score = r.Score
            }).ToList();

            return View(viewModels);
        }
    }
}
