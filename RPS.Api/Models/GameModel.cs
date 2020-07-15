﻿using Microsoft.AspNetCore.Mvc;
using System;

namespace RPS.Api.Models
{
    public class GameModel
    {
        public GameModel(Guid gameId)
        {
            Id = gameId;
            Player1 = new PlayerModel();
            Player2 = new PlayerModel();
        }

        public Guid Id { get; }

        public PlayerModel Player1 { get; set; }

        public PlayerModel Player2 { get; set; }

        //public string Player1Name { get; set; }

        //public int Player1Score { get; set; } = 0;

        //public Weapon Player1Selection { get; set; }

        //public string Player2Name { get; set; }

        //public int Player2Score { get; set; } = 0;

        //public Weapon Player2Selection { get; set; }

        public Winner LastWinner { get; set; }
    }
}
