using BLL.Interfaces;
using Domain.Minesweeper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GameSessionService : IGameSessionService
    {
        private readonly ConcurrentDictionary<Guid, Board> _games = new();
        private readonly ConcurrentDictionary<Guid, DateTime> _gameStartTimes = new();

        public Guid StartNewGame(int width, int height, int minesCount)
        {
            var id = Guid.NewGuid();
            var board = new Board(width, height, minesCount);
            _games.TryAdd(id, board);
            _gameStartTimes.TryAdd(id, DateTime.UtcNow);
            return id;
        }

        public bool TryGetBoard(Guid gameId, out Board board)
            => _games.TryGetValue(gameId, out board);
        public DateTime? GetStartTime(Guid gameId)
        {
            if (_gameStartTimes.TryGetValue(gameId, out var startTime))
                return startTime;
            return null;
        }
        public int CalculateScore(Guid gameId, bool isWin, double durationSeconds)
        {
            if (!isWin) return 0;

            if (!_games.TryGetValue(gameId, out var board)) return 0;

            int openedCells = board.GetOpenedCellsCount();
            return openedCells * 10;
        }
    }
}
