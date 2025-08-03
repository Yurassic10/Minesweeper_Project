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
    public interface IPlayerRepository
    {
        Task<Player> GetByNameAsync(string name);
        Task AddPlayerAsync(Player player);
        Task<List<Player>> GetAllAsync();
    }
}
