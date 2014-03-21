using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PhoneGameService.Models;
using PhoneGameService.Repositories;
using PhoneGameService.Services;
using PhoneGameService.Models.EdgeConditionals;

namespace PhoneGameService.Tests
{
    [TestFixture]
    public class PickPhraseTests
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

                EdgeConditional edge = _newGame.Edges.First<EdgeConditional>(e => e.nextNode.GetType().Name.Equals("PickPlayer"));

                Assert.That(GameService.TransitionGameState(_newGame, edge, repository), "Transition failed");

                _player2 = GameService.FindPlayer("15022967466", repository);
                GameService.AddPlayerToGame(_player2, _newGame, repository);

            }
        }

        [TestFixtureTearDown]
        public void TeardownTests()
        {
        }

        [Test]
        public void Transition_to_PickPhrase()
        {
            using (var repository = new TelephoneGameRepository())
            {
                EdgeConditional edge = _newGame.Edges.First<EdgeConditional>(e => e.nextNode.GetType().Name.Equals("PickPhrase"));
                Assert.That(GameService.TransitionGameState(_newGame, edge, repository), "Transition failed");
            }
        }

    }
}
