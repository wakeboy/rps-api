using RPS.Api.Models;
using System;

namespace RPS.Api.Services
{
    public interface IGameService
    {
        bool PlayersReady(Guid gameId);

        GameModel UserPick(Guid gameId, string player, Weapon weapon);

        GameModel CheckResult(Guid gameId);

        GameModel CreateGame(string playerName);

        GameModel JoinGame(Guid gameId, string playerName);

        GameModel GetGame(Guid gameId);
    }
}
