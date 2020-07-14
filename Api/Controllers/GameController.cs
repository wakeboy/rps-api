using System;
using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository gameRepository;

        public GameController(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        [HttpPost("create")]
        public ActionResult CreateGame([FromBody] CreateGameModel createGameModel)
        {
            var game = this.gameRepository.CreateGame(createGameModel.Player1Name);
            return Ok(game);
        }

        [HttpPost("add-player")]
        public ActionResult AddPlayer([FromBody] AddPlayerModel addPlayerModel)
        {
            var game = this.gameRepository.AddPlayer(addPlayerModel.GameId, addPlayerModel.Player2Name);

            if (game == null)
                return NotFound();

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
