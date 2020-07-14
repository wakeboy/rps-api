using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
    public class GameRepository : IGameRepository
    {
        private List<GameModel> Games = new List<GameModel>();

        public GameModel AddPlayer(Guid gameId, string player2Name)
        {
            var game = Games.FirstOrDefault(g => g.Id == gameId);

            if (game == null)
            {
                return null;
            }

            game.Player2Name = player2Name;
            return game;
        }

        public GameModel CreateGame(string player1Name)
        {
            var game = new GameModel
            {
                Id = Guid.NewGuid(),
                Player1Name = player1Name
            };

            Games.Add(game);
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
    }
}
