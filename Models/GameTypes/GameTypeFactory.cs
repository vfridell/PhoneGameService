using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneGameService.Models.GameTypes
{
    public class GameTypeFactory
    {
        private static Dictionary<Type, GameType> _gameTypes = new Dictionary<Type,GameType>();

        static GameTypeFactory()
        {
            _gameTypes.Add(typeof(TwoPlayersOriginal), new TwoPlayersOriginal());
            _gameTypes.Add(typeof(TestingGameType), new TestingGameType());
            foreach (GameType gt in _gameTypes.Values)
            {
                gt.Initialize();
            }
        }

        public static T GetGameType<T>() where T : GameType
        {
            return (T)_gameTypes[typeof(T)];
        }

        public static IList<GameType> GetAllGameTypes()
        {
            return _gameTypes.Values.ToList<GameType>();
        }
    }
}
