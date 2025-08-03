using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public Task<Player> GetByNameAsync(string name)
        {
            return _playerRepository.GetByNameAsync(name);
        }

        public Task AddPlayerAsync(Player player)
        {
            return _playerRepository.AddPlayerAsync(player);
        }

        public async Task<List<Player>> GetAllAsync()
        {
            return await _playerRepository.GetAllAsync();
        }
    }
}