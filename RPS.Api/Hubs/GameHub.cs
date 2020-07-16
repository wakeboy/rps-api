using RPS.Api.Data;
using RPS.Api.Models;
using RPS.Api.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPS.Api.Hubs
{
    public class GameHub : Hub
    {
        private readonly IGameService gameService;

        public GameHub(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public async Task JoinGroup(Guid gameId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId.ToString());
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());

            if (gameService.PlayersReady(gameId))
            {
                await NotifyPlayers(gameId, "Ready");
                await NotifyGame(gameId);
            }
            else
            {
                await NotifyPlayers(gameId, "Waiting Players");
            }
        }

        public async Task NotifyPlayers(Guid gameId, string message)
        {
            await Clients.Group(gameId.ToString()).SendAsync("ReceiveMessage", message);
        }

        public async Task Pick(Guid gameId, string user, Weapon weapon)
        {
            gameService.UserPick(gameId, user, weapon);

            var game = gameService.CheckResult(gameId);
            await NotifyGame(gameId);
        }

        private async Task NotifyGame(Guid gameId)
        {
            var game = gameService.GetGame(gameId);
            await Clients.Group(gameId.ToString()).SendAsync("RecieveGame", game);
        }
    }
}
