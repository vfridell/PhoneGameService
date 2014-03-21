using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhoneGameService.Repositories;
using PhoneGameService.Models.GameStates;
using PhoneGameService.Models.EdgeConditionals;

namespace PhoneGameService.Models
{
    public abstract class GameType
    {
        public abstract int id { get; }
        public abstract string name { get; }
        public abstract string description { get; }
        public abstract int maxNumberOfPlayers { get; }
        public abstract int minNumberOfPlayers { get; }
        internal abstract GameType Initialize();

        protected Dictionary<int, GameStateNode> nodes = new Dictionary<int, GameStateNode>();

        public GameStateNode startNode { get; set; }
        public abstract GameStateNode GetNode(int id);


        protected int _nodeId = 1;
        protected int _edgeId = 1;

        protected N AddNode<N>(int activePlayer)
            where N : GameStateNode, new()
        {
            N newNode = new N() { id = _nodeId++, activePlayerNumber = activePlayer };
            nodes.Add(newNode.id, newNode);
            return newNode;
        }

        protected EdgeConditional AddEdge<E>(GameStateNode startNode, GameStateNode endNode, string edgeText)
            where E : EdgeConditional, new()
        {
            E newEdge = new E() { id = _edgeId++, nextNode = endNode, text = edgeText };
            startNode.edgeConditionals.Add(newEdge);
            return newEdge;
        }
    }
}
