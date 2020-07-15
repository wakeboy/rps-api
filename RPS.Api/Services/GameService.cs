using RPS.Api.Data;
using RPS.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPS.Api.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public GameModel CheckResult(Guid gameId)
        {
            var game = gameRepository.GetGame(gameId);

            if (game.Player1.Selection == Weapon.None || game.Player2.Selection == Weapon.None)
            {
                return null;
            }

            switch(game.Player1.Selection.ToString().ToLower() + game.Player2.Selection.ToString().ToLower())
            {
                case "rockscissors":
                case "paperrock":
                case "scissorspaper":
                    game.LastWinner = Winner.Player1;
                    game.Player1.Score++;
                    break;
                case "rockpaper":
                case "scissorsrock":
                case "paperscissors":
                    game.LastWinner = Winner.Player2;
                    game.Player2.Score++;
                    break;
                default:
                    game.LastWinner = Winner.Draw;
                    break;
            }

            game.Player1.PreviousSelection = game.Player1.Selection;
            game.Player1.Selection = Weapon.None;
            game.Player2.PreviousSelection = game.Player2.Selection;
            game.Player2.Selection = Weapon.None;

            gameRepository.UpdateGame(game);
            return game;
        }

        public GameModel CreateGame(string playerName)
        {
            var game = new GameModel(Guid.NewGuid());
            game.Player1.Name = playerName;
            return this.gameRepository.AddGame(game);
        }

        public GameModel GetGame(Guid gameId)
        {
            return gameRepository.GetGame(gameId);
        }

        public GameModel JoinGame(Guid gameId, string playerName)
        {
            var game = this.gameRepository.GetGame(gameId);

            if (game == null)
                throw new ArgumentException("Games does not exist", nameof(gameId));

            if (game.Player1.Name == playerName)
                throw new ArgumentException("Player name already exists.", nameof(playerName));

            if (!string.IsNullOrEmpty(game.Player2.Name))
                throw new Exception("Maximum nubmer of players already in game");

            game.Player2.Name = playerName;
            return gameRepository.UpdateGame(game);
        }

        public bool PlayersReady(Guid gameId)
        {
            var game = gameRepository.GetGame(gameId);

            if (string.IsNullOrEmpty(game.Player1.Name) || 
                string.IsNullOrEmpty(game.Player2.Name))
            {
                return false;
            }

            return true;
        }

        public GameModel UserPick(Guid gameId, string player, Weapon weapon)
        {
            var game = gameRepository.GetGame(gameId);

            if(game.Player1.Name == player)
            {
                game.Player1.Selection = weapon;
            }
            else if (game.Player2.Name == player)
            {
                game.Player2.Selection = weapon;
            }

            return gameRepository.UpdateGame(game);
        }
    }
}
