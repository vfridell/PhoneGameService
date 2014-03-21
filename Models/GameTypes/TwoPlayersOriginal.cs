using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhoneGameService.Repositories;
using PhoneGameService.Models.GameStates;
using PhoneGameService.Models.EdgeConditionals;

namespace PhoneGameService.Models
{
    public class TwoPlayersOriginal : GameType
    {
        internal TwoPlayersOriginal() { }


        public override int id
        {
            get { return 1; }
        }

        public override string name
        {
            get { return "Original"; }
        }

        public override string description
        {
            get { return "Two players try to guess what was said."; }
        }

        public override int maxNumberOfPlayers
        {
            get { return 2; }
        }

        public override int minNumberOfPlayers
        {
            get { return 2; }
        }

        internal override GameType Initialize()
        {
            var notStarted = AddNode<NotStarted>(1);
            var pickPlayer2 = AddNode<PickPlayer>(1);
            var pickPhrase = AddNode<PickPhrase>(1);
            var player1Ready1 = AddNode<PlayerReady>(1);
            var player1SpeakPhrase = AddNode<PlayerSpeakPhrase>(1);
            var notifyPlayer2 = AddNode<NotifyPlayer>(2);
            var player2Ready = AddNode<PlayerReady>(2);
            var listenSpeakAnswer = AddNode<PlayerListenSpeakAnswer>(2);
            var notifyPlayer1 = AddNode<NotifyPlayer>(1);
            var player1Ready2 = AddNode<PlayerReady>(1);
            var listenAnswer = AddNode<PlayerListenAnswer>(1);
            var endGame = AddNode<EndGame>(0);

            AddEdge<NoCondition>(notStarted, pickPlayer2, "Start");
            AddEdge<CheckNumberOfPlayers>(pickPlayer2, pickPhrase, "Player 2 chosen");
            AddEdge<CheckPhraseChosen>(pickPhrase, player1Ready1, "Phrase chosen");
            AddEdge<NoCondition>(player1Ready1, player1SpeakPhrase, "Player1 ready");
            AddEdge<NoCondition>(player1Ready1, pickPhrase, "Pick a different phrase");
            AddEdge<NoCondition>(player1Ready1, pickPlayer2, "Pick a different player2");
            AddEdge<CheckError>(player1SpeakPhrase, notifyPlayer2, "No error");
            AddEdge<NoCondition>(player1SpeakPhrase, player1Ready1, "Error");
            AddEdge<NoCondition>(notifyPlayer2, player2Ready, "Player1 turn end");
            AddEdge<NoCondition>(player2Ready, listenSpeakAnswer, "Player2 ready");
            AddEdge<CheckError>(listenSpeakAnswer, notifyPlayer1, "No error");
            AddEdge<NoCondition>(listenSpeakAnswer, player2Ready, "Error");
            AddEdge<NoCondition>(notifyPlayer1, player1Ready2, "Player2 turn end");
            AddEdge<NoCondition>(player1Ready2, listenAnswer, "Player1 ready");
            AddEdge<CheckError>(listenAnswer, endGame, "No error");
            AddEdge<NoCondition>(listenAnswer, player1Ready2, "Error");

            startNode = notStarted;

            return this;
        }

        public override GameStateNode GetNode(int id)
        {
            return nodes[id];
        }

        internal GameStateNode GetNextGameState(Game game, int currentNodeId, int edgeId)
        {
            try
            {
                GameStateNode node = nodes[currentNodeId];
                EdgeConditional edge = node.edgeConditionals.First<EdgeConditional>(e => e.id == edgeId);
                return edge.nextNode;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Could not get next GameStateNode.  Node id: {0} edge id: {1}", currentNodeId, edgeId), ex);
            }
        }

        internal bool TryGetNextGameState(Game game, int currentNodeId, int edgeId, out GameStateNode nextNode)
        {
            try
            {
                GameStateNode node = nodes[currentNodeId];
                EdgeConditional edge = node.edgeConditionals.First<EdgeConditional>(e => e.id == edgeId);
                nextNode = edge.nextNode;
                return true;
            }
            catch (Exception ex)
            {
                nextNode = null;
                return false;
            }
        }
    }
}
