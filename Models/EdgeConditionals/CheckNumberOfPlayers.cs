using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneGameService.Models.EdgeConditionals
{
    public class CheckNumberOfPlayers : EdgeConditional
    {
        public override bool Transition(Game game, Repositories.TelephoneGameRepository repository)
        {
            if (game.numberOfPlayers >= game.gameType.minNumberOfPlayers)
            {
                ChangeState(game, repository);
                return true;
            }

            return false;
        }
    }
}
