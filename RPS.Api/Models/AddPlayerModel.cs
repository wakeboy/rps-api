using System;

namespace RPS.Api.Models
{
    public class AddPlayerModel
    {
        public Guid GameId { get; set; }

        public string PlayerName { get; set; }
    }
}