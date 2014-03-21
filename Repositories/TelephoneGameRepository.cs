using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhoneGameService.Models;
using PhoneGameService.Models.GameTypes;

namespace PhoneGameService.Repositories
{
    public class TelephoneGameRepository : IDisposable
    {
        #region IDisposable Members
        public void Dispose()
        {
        }
        #endregion

        private static Player[] _players = { new Player { ID="70C5AB60-F65C-4319-85B4-2DF3398E24DC", Name="Vern Fridell", TelephoneNumber= new PhoneNumber() { ID=1, Number="15022967010"} },
                                             new Player { ID="34C94F02-7F26-4C5B-8C5D-3C64DEB979A3", Name="AJ Ruf", TelephoneNumber= new PhoneNumber() { ID=2, Number="15022967466"} }, 
                                             new Player { ID="67516D71-8B31-4B1F-8DC2-6A0E60E92605", Name="Ellie Fridell", TelephoneNumber= new PhoneNumber() { ID=3, Number="15027625695"} }, 
                                           };
        private static GameType[] _gameTypes = { };
        private static PlayerCreationRequest[] _creationRequests = { };

        private static GamePhrase[] _gamePhrases = { new GamePhrase() { id=1, text = "blah" }, new GamePhrase() { id=2, text = "blah2" } };

        public TelephoneGameRepository()
        {
        }

        internal IList<Player> GetPlayers()
        {
            return _players.ToList<Player>();
        }

        internal Player GetPlayerByID(string ID)
        {
            return _players.FirstOrDefault<Player>(p => p.ID == ID);
        }

        internal Player GetPlayerByPhoneNumber(PhoneNumber number)
        {
            return _players.FirstOrDefault<Player>(p => p.TelephoneNumber.CompareTo(number) == 0);
        }

        internal Player GetPlayerByPhoneNumber(string number)
        {
            return _players.FirstOrDefault<Player>(p => p.TelephoneNumber.CompareTo(number) == 0);
        }

        internal IList<GameType> GetAllGameTypes()
        {
            return GameTypeFactory.GetAllGameTypes();
        }

        internal GameType GetGameType<T>() where T : GameType
        {
            return GameTypeFactory.GetGameType<T>();
        }

        internal IList<GamePhrase> GetAllGamePhrases()
        {
            return _gamePhrases.ToList();
        }
    }
}
