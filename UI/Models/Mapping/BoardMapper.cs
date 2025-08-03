using Domain.Minesweeper;
using UI.Models.ViewModels;


///<summary>
/// In order to map the data received from 
/// the client side with the models on the server
/// </summary>

namespace UI.Models.Mapping
{
    public static class BoardMapper
    {
        public static BoardViewModel ToViewModel(Board board)
        {
            var viewModel = new BoardViewModel
            {
                Rows = board.Rows,
                Columns = board.Columns,
                MinesCount = board.MinesCount,
                Cells = new List<CellViewModel>()
            };

            for (int r = 0; r < board.Rows; r++)
            {
                for (int c = 0; c < board.Columns; c++)
                {
                    var cell = board.GetCell(r, c);
                    viewModel.Cells.Add(new CellViewModel
                    {
                        Row = cell.Row,
                        Col = cell.Col,
                        HasMine = cell.HasMine,
                        AdjacentMines = cell.AdjacentMines,
                        IsOpened = cell.IsOpened,
                        IsFlagged = cell.IsFlagged
                    });
                }
            }

            return viewModel;
        }
    }
}
