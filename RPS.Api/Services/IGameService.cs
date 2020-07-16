using RPS.Api.Models;
using System;
using System.Collections.Generic;

namespace RPS.Api.Services
{
    public interface IGameService
    {
        bool PlayersReady(Guid gameId);

        GameModel UserPick(Guid gameId, string player, Weapon weapon);

        GameModel CheckResult(Guid gameId);

        GameModel CreateGame(string playerName);

        (GameModel game, List<string> errors) JoinGame(Guid gameId, string playerName);

        GameModel GetGame(Guid gameId);
    }
}
