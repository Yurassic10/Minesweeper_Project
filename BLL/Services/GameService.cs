using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Domain.Minesweeper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace BLL.Services
{
    public class GameService : IGameService
    {

        private readonly IGameSessionService _gameSession;
        private readonly IGameResultRepository _gameResultRepository;

        public GameService(IGameSessionService gameSession, IGameResultRepository gameResultRepository)
        {
            _gameResultRepository = gameResultRepository;
            _gameSession = gameSession;
        }

        public async Task<List<GameResult>> GetAllAsync()
        {
            return await _gameResultRepository.GetAllAsync();
        }

        Task IGameService.AddGameResultAsync(GameResult result)
        {
            return _gameResultRepository.AddGameResultAsync(result);
        }

        public Board? GetBoard(Guid gameId)
        {
            _gameSession.TryGetBoard(gameId, out var board);
            return board;
        }

        Task<List<GameResult>> IGameService.GetTopResultsAsync(int count)
        {
            return _gameResultRepository.GetTopResults(count);
        }

        bool IGameService.IsLost(Guid gameId)
        {
            if (_gameSession.TryGetBoard(gameId, out var board))
                return board.IsLost();

            throw new ArgumentException("Game not found");
        }

        bool IGameService.IsWin(Guid gameId)
        {
            if (_gameSession.TryGetBoard(gameId, out var board))
                return board.IsWin();

            throw new ArgumentException("Game not found");
        }

        bool IGameService.OpenCell(Guid gameId, int row, int col)
        {
            if (_gameSession.TryGetBoard(gameId, out var board))
            {
                return board.OpenCell(row, col);
            }
            throw new ArgumentException("Game not found");
        }
        public Guid StartGame(int width, int height, int minesCount)
        => _gameSession.StartNewGame(width, height, minesCount);

        void IGameService.ToggleFlag(Guid gameId, int row, int col)
        {
            if (_gameSession.TryGetBoard(gameId, out var board))
            {
                board.ToggleFlag(row, col);
            }
            else
            {
                throw new ArgumentException("Game not found");
            }
        }

        public DateTime GetStartTime(Guid gameId)
        {
            var startTime = _gameSession.GetStartTime(gameId);
            if (startTime.HasValue)
                return startTime.Value;
            throw new ArgumentException("Game not found or start time missing");
        }
        public int CalculateScore(Guid gameId, bool isWin, double durationSeconds)
        {
            if (!isWin) return 0;

            if (!_gameSession.TryGetBoard(gameId, out var board)) return 0;

            int openedCells = board.GetOpenedCellsCount();
            return openedCells * 10;
        }
    }
}
