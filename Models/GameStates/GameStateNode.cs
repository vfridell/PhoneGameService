using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhoneGameService.Models.EdgeConditionals;

namespace PhoneGameService.Models.GameStates
{
    public class GameStateNode
    {
        public virtual string routeName { get { return this.GetType().Name; } }
        public IList<EdgeConditional> edgeConditionals = new List<EdgeConditional>();
        public int activePlayerNumber { get; set; }
        public int id { get; set; }
    }
}
