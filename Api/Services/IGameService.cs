﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IGameService
    {
        bool PlayersReady(Guid gameId);
    }
}
