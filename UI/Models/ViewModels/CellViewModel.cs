namespace UI.Models.ViewModels
{
    public class CellViewModel
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public bool HasMine { get; set; }
        public int AdjacentMines { get; set; }
        public bool IsOpened { get; set; }
        public bool IsFlagged { get; set; }
    }
}
