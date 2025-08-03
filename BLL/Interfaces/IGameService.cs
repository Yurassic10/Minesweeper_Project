using DAL.Entities;
using Domain.Minesweeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///<summary>
/// Game development service and support methods
/// </summary>

namespace BLL.Interfaces
{
    public interface IGameService
    {
        Guid StartGame(int rows, int columns, int minesCount);
        bool OpenCell(Guid gameId, int row, int col);

        void ToggleFlag(Guid gameId, int row, int col);

        Board GetBoard(Guid gameId);

        bool IsWin(Guid gameId);

        bool IsLost(Guid gameId);
        Task AddGameResultAsync(GameResult result);
        Task<List<GameResult>> GetTopResultsAsync(int count);
        Task<List<GameResult>> GetAllAsync();
        DateTime GetStartTime(Guid gameId);
        int CalculateScore(Guid gameId, bool isWin, double durationSeconds);
    }
}
