using Api.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Hubs
{
    public class GameHub : Hub
    {
        private readonly IGameService gameService;

        public GameHub(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public async Task Echo(string message)
        {
            await Clients.All.SendAsync("Send", message);
        }

        public async Task JoinGroup(Guid group)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group.ToString());
            await Groups.AddToGroupAsync(Context.ConnectionId, group.ToString());

            if (gameService.PlayersReady(group))
                await Clients.Group(group.ToString()).SendAsync("ReceiveMessage", "Ready");
            else
                await Clients.Group(group.ToString()).SendAsync("ReceiveMessage", "Waiting Players");
        }

        public async Task NotifyPlayerReady(Guid group, string message)
        {
            await Clients.Group(group.ToString()).SendAsync("ReceiveMessage", message);
        }
    }
}
