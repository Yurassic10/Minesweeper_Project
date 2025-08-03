using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///<summary>
///In fact, the entire logic of the game
/// </summary>

namespace Domain.Minesweeper
{
    public class Board
    {
        public int Rows { get; }
        public int Columns { get; }
        public int MinesCount { get; }
        private readonly Cell[,] _cells;

        public Board(int rows, int columns, int minesCount)
        {
            if (minesCount >= rows * columns)
                throw new ArgumentException("Mines count must be less than total cells");

            Rows = rows;
            Columns = columns;
            MinesCount = minesCount;

            _cells = new Cell[rows, columns];
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Columns; c++)
                    _cells[r, c] = new Cell(r, c);

            PlaceMines();
            CalculateAdjacentMines();
        }

        public int GetOpenedCellsCount()
        {
            return _cells.Cast<Cell>().Count(cell => cell.IsOpened);
        }
        private void PlaceMines() // random placement of mines on the field
        {
            var rand = new Random();
            int placed = 0;
            while (placed < MinesCount)
            {
                int r = rand.Next(Rows);
                int c = rand.Next(Columns);

                if (!_cells[r, c].HasMine)
                {
                    _cells[r, c].HasMine = true;
                    placed++;
                }
            }
        }

        private void CalculateAdjacentMines() //Calculates for each cell how many mines are located nearby
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    if (_cells[r, c].HasMine)
                    {
                        _cells[r, c].AdjacentMines = -1;
                        continue;
                    }

                    int count = 0;
                    foreach (var n in GetNeighbors(r, c))
                        if (n.HasMine) count++;

                    _cells[r, c].AdjacentMines = count;
                }
            }
        }

        public IEnumerable<Cell> GetNeighbors(int row, int col) //Returns a list of neighboring cells around a given cell with coordinates
        {
            ///<summary>
            ///dr = -1..1 and dc = -1..1 are the offsets (deltas) 
            ///to check all neighboring cells around 
            ///the current one: above, below, left, right and diagonally
            /// </summary>
            for (int dr = -1; dr <= 1; dr++) //delta row
                for (int dc = -1; dc <= 1; dc++) // delta column
                {
                    if (dr == 0 && dc == 0) continue;
                    int nr = row + dr; // neighbor row
                    int nc = col + dc; // neighbor column
                    if (nr >= 0 && nr < Rows && nc >= 0 && nc < Columns)
                        yield return _cells[nr, nc];
                }
        }

        public Cell GetCell(int row, int col) => _cells[row, col];

        public bool OpenCell(int row, int col) // Opens the cell if possible
        {
            var cell = GetCell(row, col);
            if (cell.IsOpened || cell.IsFlagged) return false;

            cell.IsOpened = true;

            if (cell.AdjacentMines == 0 && !cell.HasMine)
                foreach (var n in GetNeighbors(row, col))
                    if (!n.IsOpened)
                        OpenCell(n.Row, n.Col);

            return true;
        }

        public void ToggleFlag(int row, int col) //Checks or unchecks a cell.
        {
            var cell = GetCell(row, col);
            if (cell.IsOpened) return;
            cell.IsFlagged = !cell.IsFlagged;
        }

        public bool IsWin()
        {
            foreach (var cell in _cells)
                if (!cell.HasMine && !cell.IsOpened)
                    return false;
            return true;
        }

        public bool IsLost()
        {
            foreach (var cell in _cells)
                if (cell.HasMine && cell.IsOpened)
                    return true;
            return false;
        }
    }
}
