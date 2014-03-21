using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneGameService.Models.EdgeConditionals
{
    public class NoCondition : EdgeConditional
    {
        public override bool Transition(Game game, Repositories.TelephoneGameRepository repository)
        {
            ChangeState(game, repository);
            return true;
        }
    }
}
