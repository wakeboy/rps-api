using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
    public interface IGameRepository
    {
        GameModel CreateGame(string player1Name);

        GameModel AddPlayer(Guid gameId, string player2Name);

        GameModel GetGame(Guid id);

        List<GameModel> GetGames();
    }
}
