using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///<summary>
///Database context for interacting with data
/// </summary>

namespace DAL.DB
{
    public class MinesweeperDbContext : DbContext
    {
        public MinesweeperDbContext(DbContextOptions<MinesweeperDbContext> options):base(options)
        {
            
        }

        public DbSet<Player> Players => Set<Player>();
        public DbSet<GameResult> GameResults => Set<GameResult>();
    }
}
