namespace UI.Models.ViewModels
{
    public class BoardViewModel
    {
        public Guid GameId { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int MinesCount { get; set; }
        public List<CellViewModel> Cells { get; set; } = new();
    }
}
