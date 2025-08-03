using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using UI.Models.ViewModels;

namespace UI.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public async Task<IActionResult> Index()
        {
            var players = await _playerService.GetAllAsync();
            var viewModels = players.Select(p => new PlayerViewModel
            {
                Id = p.Id,
                Name = p.Name,
                RegisteredAt = p.RegisteredAt
            }).ToList();

            return View(viewModels);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(PlayerViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _playerService.AddPlayerAsync(new Player
            {
                Name = model.Name,
                RegisteredAt = DateTime.UtcNow
            });

            return RedirectToAction(nameof(Index));
        }
    }
}
