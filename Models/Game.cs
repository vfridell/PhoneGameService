using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using PhoneGameService.Repositories;
using PhoneGameService.Models.GameStates;
using PhoneGameService.Models.EdgeConditionals;

namespace PhoneGameService.Models
{
    public class Game
    {
        public int ID { get; set; }

        internal GameType _gameType { get; set; }
        public GameType gameType { get { return _gameType; } }

        internal int _currentNodeNumber;
        public int currentNodeNumber { get { return _currentNodeNumber; } }

        public GameStateNode currentNode { get { return gameType.GetNode(_currentNodeNumber); } }

        internal GamePhrase _gamePhrase;
        public GamePhrase gamePhrase { get { return _gamePhrase; } }

        internal bool _error = false;
        public bool error { get { return _error; } }

        private Dictionary<int, Player> _players = new Dictionary<int, Player>();
        private int _nextPlayerNumber = 1;
        public IDictionary<int, Player> players { get { return _players; } }

        public IList<EdgeConditional> Edges { get { return gameType.GetNode(currentNodeNumber).edgeConditionals; } }

        public bool enoughPlayers { get { return _players.Count >= minNumberOfPlayers; } }
        public int numberOfPlayers { get { return _players.Count; } }
        public int maxNumberOfPlayers { get { return _gameType.maxNumberOfPlayers; } }
        public int minNumberOfPlayers { get { return _gameType.minNumberOfPlayers; } }
        
        internal void AddPlayer(Player player, TelephoneGameRepository repository)
        {
            if (_players.Count >= gameType.maxNumberOfPlayers)
            {
                throw new Exception(string.Format("Cannot add more than the maximum number of players for this game type: {0}", gameType.maxNumberOfPlayers));
            }

            if (null != _players.Values.FirstOrDefault<Player>(p => p.ID == player.ID))
            {
                throw new Exception("Player is already added to this game!");
            }

            _players[_nextPlayerNumber++] = player;
        }

        internal void PickPhrase(GamePhrase phrase, TelephoneGameRepository repository)
        {
            _gamePhrase = phrase;
        }

    }
}
