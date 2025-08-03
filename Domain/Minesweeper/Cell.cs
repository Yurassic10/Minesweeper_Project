using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///<summary>
///A model that has cell information
/// </summary>

namespace Domain.Minesweeper
{
    public class Cell
    {
        public int Row { get; }
        public int Col { get; }
        public bool HasMine { get; set; }
        public int AdjacentMines { get; set; }
        public bool IsOpened { get; set; }
        public bool IsFlagged { get; set; }

        public Cell(int row, int col)
        {
            Row = row;
            Col = col;
            IsOpened = false;
            IsFlagged = false;
        }
    }
}
