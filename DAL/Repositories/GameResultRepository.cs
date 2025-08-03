using DAL.DB;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class GameResultRepository : IGameResultRepository
    {
        private readonly MinesweeperDbContext _context;
        public GameResultRepository(MinesweeperDbContext context)
        {
            _context = context;
        }

        public async Task<List<GameResult>> GetAllAsync()
        {
            return await _context.GameResults.Include(gr => gr.Player).ToListAsync();
        }

        async Task IGameResultRepository.AddGameResultAsync(GameResult result)
        {
            await _context.GameResults.AddAsync(result);
            await _context.SaveChangesAsync();
        }

        async Task<List<GameResult>> IGameResultRepository.GetTopResults(int count)
        {
            return await _context.GameResults.OrderByDescending(x => x.Score).Take(count).ToListAsync();
        }
    }
}
