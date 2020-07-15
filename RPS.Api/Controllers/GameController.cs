using System;
using RPS.Api.Data;
using RPS.Api.Models;
using RPS.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RPS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService gameService;
        private readonly IGameRepository gameRepository;
        private readonly ILogger<GameController> logger;

        public GameController(IGameService gameService, IGameRepository gameRepository, ILogger<GameController> logger)
        {
            this.gameService = gameService;
            this.gameRepository = gameRepository;
            this.logger = logger;
        }

        [HttpPost("create")]
        public ActionResult CreateGame([FromBody] CreateGameModel createGameModel)
        {
            var game = this.gameService.CreateGame(createGameModel.PlayerName);
            return Ok(game);
        }

        [HttpPost("join-game")]
        public ActionResult JoinGame([FromBody] AddPlayerModel addPlayerModel)
        {
            var game = this.gameService.JoinGame(addPlayerModel.GameId, addPlayerModel.PlayerName);

            return Ok(game);
        }

        [HttpGet("{id}")]
        public ActionResult GetGame(Guid id)
        {
            var game = this.gameRepository.GetGame(id);
            
            if (game == null)
                return NotFound();

            return Ok(game);
        }
    }
}
