using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPS.Api.Models
{
    public enum Winner
    {
        None = 1,
        Player1,
        Player2,
        Draw
    }

    public enum Weapon
    {
        None = 1,
        Rock,
        Paper,
        Scissors
    }
}
