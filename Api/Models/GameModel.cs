using System;

namespace Api.Models
{
    public class GameModel
    {
        public Guid Id { get; set; }

        public string Player1Name { get; set; }

        public string Player2Name { get; set; }

        public int Player1Score { get; set; } = 0;

        public int Player2Score { get; set; } = 0; 
    }
}
