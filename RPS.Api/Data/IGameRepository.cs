using RPS.Api.Models;
using System;
using System.Collections.Generic;

namespace RPS.Api.Data
{
    public interface IGameRepository
    {
        GameModel AddGame(GameModel game);

        GameModel UpdateGame(GameModel game);

        GameModel GetGame(Guid id);

        List<GameModel> GetGames();
    }
}
