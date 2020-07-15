using Microsoft.Extensions.Logging;
using RPS.Api.Exceptions;
using RPS.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RPS.Api.Data
{
    public class GameRepository : IGameRepository
    {
        private readonly ILogger<GameRepository> logger;
        private List<GameModel> Games = new List<GameModel>();

        public GameRepository(ILogger<GameRepository> logger)
        {
            this.logger = logger;
        }

        public GameModel AddGame(GameModel game)
        {
            var existingGame = Games.FirstOrDefault(g => g.Id == game.Id);
            if (existingGame != null)
            {
                logger.LogError("Game already exists failed to add game");
                throw new GameExistsException();
            }

            Games.Add(game);
            return game;
        }

        public GameModel AddPlayer(Guid gameId, string player2Name)
        {
            var game = Games.FirstOrDefault(g => g.Id == gameId);

            if (game == null)
            {
                return null;
            }

            game.Player2.Name = player2Name;
            return game;
        }

        public GameModel GetGame(Guid id)
        {
            return Games.FirstOrDefault(g => g.Id == id);
        }

        public List<GameModel> GetGames()
        {
            return Games;
        }

        public GameModel UpdateGame(GameModel game)
        {
            Games.Remove(GetGame(game.Id));
            Games.Add(game);
            return GetGame(game.Id);
        }
    }
}
