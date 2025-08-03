using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class GameResult
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int DurationSeconds { get; set; }
        public string Result { get; set; } = string.Empty; // 'win' or 'loss'
        public DateTime DatePlayed  { get; set; }
        public int Score {  get; set; }
        public Player? Player { get; set; }

    }
}
