using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Hubs
{
    public class GameHub : Hub
    {
        public async Task Echo(string message)
        {
            await Clients.All.SendAsync("Send", message);
        }
    }
}
