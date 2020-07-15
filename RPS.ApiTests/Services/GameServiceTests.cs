using Moq;
using NUnit.Framework;
using RPS.Api.Data;
using RPS.Api.Models;
using RPS.Api.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RPS.ApiTests.Services
{
    public class GameServiceTests
    {
        IGameService gameService;
        Mock<IGameRepository> gameRepository;

        [SetUp]
        public void SetUp()
        {
            gameRepository = new Mock<IGameRepository>();
            gameService = new GameService(gameRepository.Object);
        }

        [TestCase(Weapon.Rock, Weapon.Scissors)]
        [TestCase(Weapon.Paper, Weapon.Rock)]
        [TestCase(Weapon.Scissors, Weapon.Paper)]
        public void WhenCheckingResult_Player1ShouldWin(Weapon player1Selection, Weapon player2Selection)
        {
            var game = new GameModel(Guid.NewGuid());
            game.Player1.Name = "P1";
            game.Player1.Selection = player1Selection;
            game.Player2.Name = "P2";
            game.Player2.Selection = player2Selection;

            gameRepository.Setup(gr => gr.GetGame(game.Id)).Returns(game);

            var result = gameService.CheckResult(game.Id);

            Assert.AreEqual(Winner.Player1, result.LastWinner);
            Assert.AreEqual(1, game.Player1.Score);
            Assert.AreEqual(0, game.Player2.Score);
        }

        [TestCase(Weapon.Rock, Weapon.Paper)]
        [TestCase(Weapon.Paper, Weapon.Scissors)]
        [TestCase(Weapon.Scissors, Weapon.Rock)]
        public void WhenCheckingResult_Player2ShouldWin(Weapon player1Selection, Weapon player2Selection)
        {
            var game = new GameModel(Guid.NewGuid());
            game.Player1.Name = "P1";
            game.Player1.Selection = player1Selection;
            game.Player2.Name = "P2";
            game.Player2.Selection = player2Selection;

            gameRepository.Setup(gr => gr.GetGame(game.Id)).Returns(game);

            var result = gameService.CheckResult(game.Id);

            Assert.AreEqual(Winner.Player2, result.LastWinner);
            Assert.AreEqual(0, game.Player1.Score);
            Assert.AreEqual(1, game.Player2.Score);
        }

        [TestCase(Weapon.Rock, Weapon.Rock)]
        [TestCase(Weapon.Paper, Weapon.Paper)]
        [TestCase(Weapon.Scissors, Weapon.Scissors)]
        public void WhenCheckingResult_GameShouldDraw(Weapon player1Selection, Weapon player2Selection)
        {
            var game = new GameModel(Guid.NewGuid());
            game.Player1.Name = "P1";
            game.Player1.Selection = player1Selection;
            game.Player2.Name = "P2";
            game.Player2.Selection = player2Selection;

            gameRepository.Setup(gr => gr.GetGame(game.Id)).Returns(game);

            var result = gameService.CheckResult(game.Id);

            Assert.AreEqual(Winner.Draw, result.LastWinner);
            Assert.AreEqual(0, game.Player1.Score);
            Assert.AreEqual(0, game.Player2.Score);
        }
    }
}
