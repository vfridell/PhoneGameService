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
    public class OriginalGameStartTests
    {
        Player _player1;
        Player _player2;
        Game _newGame;

        [TestFixtureSetUp]
        public void SetupTests()
        {
            using (var repository = new TelephoneGameRepository())
            {
                bool success;

                _player1 = GameService.FindPlayer("15022967010", repository);
                _newGame = GameService.CreateNewGame<TwoPlayersOriginal>(_player1, repository);
                Assert.That(null != _newGame, "Game not created");

                success = GameService.TransitionGameState(_newGame, _newGame.Edges[0], repository);
                Assert.That(success, string.Format("Couldn't transition to {0}", _newGame.Edges[0].nextNode.routeName));

                _player2 = GameService.FindPlayer("15022967466", repository);
                GameService.AddPlayerToGame(_player2, _newGame, repository);
                Assert.That(_newGame.enoughPlayers, "not enough players added");

                success = GameService.TransitionGameState(_newGame, _newGame.Edges[0], repository);
                Assert.That(success, string.Format("Couldn't transition to {0}", _newGame.Edges[0].nextNode.routeName));
            }
        }

        [TestFixtureTearDown]
        public void TeardownTests()
        {
        }

        [Test]
        public void GameState_BeginGame()
        {
        }


    }
}
