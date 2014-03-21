using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhoneGameService.Models;
using PhoneGameService.Repositories;
using PhoneGameService.Models.GameTypes;
using PhoneGameService.Models.EdgeConditionals;

namespace PhoneGameService.Services
{
    public static class GameService
    {
        public static IList<Player> GetRecentPlayers(TelephoneGameRepository repository)
        {
            return repository.GetPlayers();
        }

        public static IList<Game> GetGames(Player player, TelephoneGameRepository repository)
        {
            throw new NotImplementedException();
        }

        public static void RequestNewPlayer(PlayerCreationRequest request, TelephoneGameRepository repository)
        {
            throw new NotImplementedException();
        }

        public static Player ValidatePlayerRequest(PlayerCreationValidation validation, TelephoneGameRepository repository)
        {
            throw new NotImplementedException();
        }

        public static IList<GameType> GetGameTypes(TelephoneGameRepository repository)
        {
            return repository.GetAllGameTypes();
        }

        public static Game CreateNewGame<T>(Player player1, TelephoneGameRepository repository) where T : GameType
        {
            Game newGame = new Game() { _gameType = GameTypeFactory.GetGameType<T>() };
            newGame.AddPlayer(player1, repository);
            newGame._currentNodeNumber = newGame._gameType.startNode.id;
            return newGame;
        }

        public static Player FindPlayer(PhoneNumber phoneNumber, TelephoneGameRepository repository)
        {
            return repository.GetPlayerByPhoneNumber(phoneNumber);
        }

        public static Player FindPlayer(string phoneNumber, TelephoneGameRepository repository)
        {
            return repository.GetPlayerByPhoneNumber(phoneNumber);
        }

        public static void AddPlayerToGame(Player player, Game game, TelephoneGameRepository repository)
        {
            game.AddPlayer(player, repository);
        }

        public static void PickPhraseForGame(GamePhrase phrase, Game game, TelephoneGameRepository repository)
        {
            game.PickPhrase(phrase, repository);
        }

        public static IList<GamePhrase> GetPhraseList(TelephoneGameRepository repository)
        {
            return repository.GetAllGamePhrases();
        }

        public static bool IsPlayersTurn(Player player, Game game, TelephoneGameRepository repository)
        {
            if (!game.players.Values.Contains<Player>(player))
            {
                throw new Exception(string.Format("Player {0} is not in game {1}", player.Name, game.ID));
            }

            KeyValuePair<int, Player> kvPair = game.players.FirstOrDefault<KeyValuePair<int, Player>>(p => p.Value.ID == player.ID);
            return kvPair.Key == game.gameType.GetNode(game.currentNodeNumber).activePlayerNumber;
        }

        public static bool TransitionGameState(Game game, EdgeConditional edge, TelephoneGameRepository repository)
        {
            return edge.Transition(game, repository);
        }

        public static void RefreshGameState(Game game, TelephoneGameRepository repository)
        {
            throw new NotImplementedException();
        }

        public static GameAudio GetGameAudio(AudioIdentifier audioID, TelephoneGameRepository repository)
        {
            throw new NotImplementedException();
        }

        public static void PutGameAudio(GameAudio audio, Game game, TelephoneGameRepository repository)
        {
            throw new NotImplementedException();
        }

        public static GameScore GetGameScore(Game game, TelephoneGameRepository repository)
        {
            throw new NotImplementedException();
        }

        public static TotalPlayerScore GetTotalPlayerScore(Player player, TelephoneGameRepository repository)
        {
            throw new NotImplementedException();
        }
    }
}