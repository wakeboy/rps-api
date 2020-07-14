using System;

namespace Api.Models
{
    public class AddPlayerModel
    {
        public Guid GameId { get; set; }

        public string Player2Name { get; set; }
    }
}