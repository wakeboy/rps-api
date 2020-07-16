using Microsoft.AspNetCore.Http.Features;
using RPS.Api.Data;
using RPS.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
                return game;
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

        public (GameModel game, List<string> errors) JoinGame(Guid gameId, string playerName)
        {
            var game = this.gameRepository.GetGame(gameId);
            var errors = new List<string>();
            if (game == null)
                errors.Add("Games does not exist");

            if (game?.Player1?.Name == playerName)
                errors.Add("Player name already exists, please user a different name");

            if (!string.IsNullOrEmpty(game?.Player2?.Name))
                errors.Add("Maximum nubmer of players already in game.");

            if (errors.Any())
                return (null, errors);

            game.Player2.Name = playerName;
            return (gameRepository.UpdateGame(game), errors);
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
