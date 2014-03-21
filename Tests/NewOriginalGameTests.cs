using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PhoneGameService.Models;
using PhoneGameService.Repositories;
using PhoneGameService.Services;

namespace PhoneGameService.Tests
{
    [TestFixture]
    public class NewOriginalGameTests
    {
        Player _player1;
        Player _player2;
        Game _newGame;

        [TestFixtureSetUp]
        public void SetupTests()
        {
            using (var repository = new TelephoneGameRepository())
            {
                _player1 = GameService.FindPlayer("15022967010", repository);
                _newGame = GameService.CreateNewGame<TwoPlayersOriginal>(_player1, repository);
                Assert.That(null != _newGame, "Game not created");

                _player2 = GameService.FindPlayer("15022967466", repository);
                GameService.AddPlayerToGame(_player2, _newGame, repository);
            }
        }

        [TestFixtureTearDown]
        public void TeardownTests()
        {
        }

        [Test]
        public void Game_Created()
        {
            Assert.That(null != _newGame, "Game not created");
        }

        [Test]
        public void Game_AddPlayer()
        {
            Assert.That(_newGame.numberOfPlayers == 2, "Player was not added");
        }

        [Test]
        [ExpectedException("System.Exception")]
        public void Game_CantAddMorePlayers()
        {
            using (var repository = new TelephoneGameRepository())
            {
                Assert.That(_newGame.maxNumberOfPlayers == _newGame.numberOfPlayers);
                Player additionalPlayer = GameService.FindPlayer("15027625695", repository);
                GameService.AddPlayerToGame(_player2, _newGame, repository);
            }
        }

        
        [Test]
        public void Game_PlayerOneTurn()
        {
            using (var repository = new TelephoneGameRepository())
            {
                Assert.That(GameService.IsPlayersTurn(_player1, _newGame, repository), "Should be player one's turn");
            }
        }
    }
}
