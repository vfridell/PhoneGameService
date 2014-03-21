using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PhoneGameService.Models;
using PhoneGameService.Repositories;
using PhoneGameService.Services;
using PhoneGameService.Models.EdgeConditionals;
using PhoneGameService.Models.GameStates;

namespace PhoneGameService.Tests
{
    [TestFixture]
    public class EdgeConditionalsTests
    {
        Player _player1;
        Player _player2;
        Game _game1;
        Game _game2;
        Game _game3;
        Game _game4;
        Game _game5;
        Game _game6;


        [TestFixtureSetUp]
        public void SetupTests()
        {
            using (var repository = new TelephoneGameRepository())
            {
                _player1 = GameService.FindPlayer("15022967010", repository);
                _player2 = GameService.FindPlayer("15022967466", repository);

                _game1 = GameService.CreateNewGame<TestingGameType>(_player1, repository);
                _game2 = GameService.CreateNewGame<TestingGameType>(_player1, repository);
                _game3 = GameService.CreateNewGame<TestingGameType>(_player1, repository);
                _game4 = GameService.CreateNewGame<TestingGameType>(_player1, repository);
                _game5 = GameService.CreateNewGame<TestingGameType>(_player1, repository);
                _game6 = GameService.CreateNewGame<TestingGameType>(_player1, repository);

                Assert.That(null != _game1, "Game1 not created");
                Assert.That(null != _game2, "Game2 not created");
                Assert.That(null != _game3, "Game3 not created");
                Assert.That(null != _game4, "Game4 not created");
                Assert.That(null != _game5, "Game4 not created");
                Assert.That(null != _game6, "Game4 not created");
            }
        }

        [TestFixtureTearDown]
        public void TeardownTests()
        {
        }

        [Test]
        public void CheckNoCondition()
        {
            using (var repository = new TelephoneGameRepository())
            {
                Game game = _game1;

                Assert.That(game.Edges[0].GetType() == typeof(NoCondition), "This edge is not a NoConditional");
                bool success = GameService.TransitionGameState(game, game.Edges[0], repository);
                Assert.That(success, "Couldn't transition along NoConditional edge");
            }
        }

        [Test]
        public void CheckError()
        {

        }

        [Test]
        public void CheckNumberOfPlayers_Success()
        {
            using (var repository = new TelephoneGameRepository())
            {
                Game game = _game2;

                bool success = GameService.TransitionGameState(game, game.Edges[0], repository);
                Assert.That(success, "Couldn't transition");

                Assert.That(game.Edges[0].GetType() == typeof(CheckNumberOfPlayers), "This edge is not a CheckNumberOfPlayers");

                GameService.AddPlayerToGame(_player2, game, repository);
                success = GameService.TransitionGameState(game, game.Edges[0], repository);
                Assert.That(success, "Couldn't transition along CheckNumberOfPlayers edge");
            }
        }

        [Test]
        public void CheckNumberOfPlayers_Failure()
        {
            using (var repository = new TelephoneGameRepository())
            {
                Game game = _game3;

                bool success = GameService.TransitionGameState(game, game.Edges[0], repository);
                Assert.That(success, "Couldn't transition");

                Assert.That(game.Edges[0].GetType() == typeof(CheckNumberOfPlayers), "This edge is not a CheckNumberOfPlayers");

                success = GameService.TransitionGameState(game, game.Edges[0], repository);
                Assert.That(!success, "Should not have transitioned along CheckNumberOfPlayers edge");
            }
        }

        [Test]
        public void CheckPhraseChosen_Success()
        {
            using (var repository = new TelephoneGameRepository())
            {
                Game game = _game4;

                bool success = GameService.TransitionGameState(game, game.Edges[1], repository);
                Assert.That(success, "Couldn't transition");

                Assert.That(game.Edges[0].GetType() == typeof(CheckPhraseChosen), "This edge is not a CheckPhraseChosen");

                var phrase = new GamePhrase();
                GameService.PickPhraseForGame(phrase, game, repository);
                success = GameService.TransitionGameState(game, game.Edges[0], repository);
                Assert.That(success, "Couldn't transition along CheckPhraseChosen edge");
            }
        }

        [Test]
        public void CheckPhraseChosen_Failure()
        {
            using (var repository = new TelephoneGameRepository())
            {
                Game game = _game5;

                bool success = GameService.TransitionGameState(game, game.Edges[1], repository);
                Assert.That(success, "Couldn't transition");

                Assert.That(game.Edges[0].GetType() == typeof(CheckPhraseChosen), "This edge is not a CheckPhraseChosen");

                success = GameService.TransitionGameState(game, game.Edges[0], repository);
                Assert.That(!success, "Should not have transitioned along CheckPhraseChosen edge");
            }
        }

    }
}
