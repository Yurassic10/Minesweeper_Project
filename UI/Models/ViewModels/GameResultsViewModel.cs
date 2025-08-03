namespace UI.Models.ViewModels
{
    public class GameResultsViewModel
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public int DurationSeconds { get; set; }
        public string Result { get; set; } = string.Empty;
        public DateTime DatePlayed { get; set; }
        public int Score { get; set; }
    }
}
