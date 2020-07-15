using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RPS.Api.Data;
using RPS.Api.Models;
using System;

namespace RPS.ApiTests.Data
{
    public class GameRepositoryTests
    {
        IGameRepository gameRepository;
        Mock<ILogger<GameRepository>> logger;

        [SetUp]
        public void SetUp()
        {
            logger = new Mock<ILogger<GameRepository>>();
            gameRepository = new GameRepository(logger.Object);
        }

        [Test]
        public void ShouldAddAGame()
        {
            var game = new GameModel(Guid.NewGuid());
            gameRepository.AddGame(game);

            var result = gameRepository.GetGame(game.Id);

            Assert.AreEqual(game, result);
        }

        [Test]
        public void ShouldAddPlayerToExistingGame()
        {
            var game = new GameModel(Guid.NewGuid());
            game.Player1.Name = "P1";
            gameRepository.AddGame(game);

            var gameToUpdate = gameRepository.GetGame(game.Id);
            gameToUpdate.Player2.Name = "P2";

            gameRepository.UpdateGame(game);

            var result = gameRepository.GetGame(game.Id);

            Assert.AreEqual(game.Id, result.Id);
            Assert.AreEqual(game.Player1.Name, game.Player1.Name);
            Assert.AreEqual(game.Player2.Name, game.Player2.Name);
        }
    }
}
