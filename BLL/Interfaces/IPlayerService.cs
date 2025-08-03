using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///Service for working with players 
///(when creating a game, there must be a player who will play)
/// <summary>
///</summary>

namespace BLL.Interfaces
{
    public interface IPlayerService
    {
        Task<Player> GetByNameAsync(string name);
        Task AddPlayerAsync(Player player);
        Task<List<Player>> GetAllAsync();
    }
}
