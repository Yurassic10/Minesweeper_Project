using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///<summary>
///The first layer for interaction purely with the database
/// </summary>

namespace DAL.Interfaces
{
    public interface IGameResultRepository
    {
        Task AddGameResultAsync(GameResult result);
        Task<List<GameResult>> GetTopResults(int count);
        Task<List<GameResult>> GetAllAsync();
    }
}
