using Domain.Minesweeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///<summary>
///Game development service and methods for working with the game grid
///
/// stores the states of active games
/// via the Dictionary<Guid, GameSession> dictionary
/// </summary>

namespace BLL.Interfaces
{
    public interface IGameSessionService
    {
        bool TryGetBoard(Guid gameId, out Board board);
        Guid StartNewGame(int width, int height, int minesCount);
        DateTime? GetStartTime(Guid gameId);

        int CalculateScore(Guid gameId, bool isWin, double durationSeconds);
    }

}
