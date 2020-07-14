using Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public bool PlayersReady(Guid gameId)
        {
            var game = gameRepository.GetGame(gameId);

            if (string.IsNullOrEmpty(game.Player1Name) || 
                string.IsNullOrEmpty(game.Player2Name))
            {
                return false;
            }

            return true;
        }
    }
}
