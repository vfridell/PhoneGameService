using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhoneGameService;
using PhoneGameService.Repositories;
using PhoneGameService.Services;
using PhoneGameService.Models;
using PhoneGameService.Models.EdgeConditionals;

namespace TestGamePlay
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var repository = new TelephoneGameRepository())
            {

                // New Game
                Player player1 = GameService.FindPlayer("15022967010", repository);
                Game newGame = GameService.CreateNewGame<TwoPlayersOriginal>(player1, repository);
                Console.WriteLine(string.Format("{0} is starting the game", player1.Name));

                // Pick Player
                EdgeConditional edge = newGame.Edges.First<EdgeConditional>(e => e.nextNode.GetType().Name.Equals("PickPlayer"));
                GameService.TransitionGameState(newGame, edge, repository);

                Player player2 = null;
                while (null == player2)
                {
                    PrintPlayers(repository);
                    Console.WriteLine("Pick a player by entering a phone number:");
                    string phonenumber = Console.ReadLine();
                    player2 = GameService.FindPlayer(phonenumber, repository);
                }
                GameService.AddPlayerToGame(player2, newGame, repository);

                Choices(newGame, repository);

                GamePhrase phrase = null;
                while (null == phrase)
                {
                    PrintPhrases(repository);
                    Console.WriteLine("Pick a phrase by choosing the number:");
                    string number = Console.ReadLine();
                    phrase = GameService.GetPhraseList(repository).FirstOrDefault<GamePhrase>(p => p.id.Equals(int.Parse(number)));
                }
                GameService.PickPhraseForGame(phrase, newGame, repository);

                while (true)
                {
                    Choices(newGame, repository);
                }

            }
        }

        static void PrintPlayers(TelephoneGameRepository repository)
        {
            foreach(Player p in GameService.GetRecentPlayers(repository))
            {
                Console.WriteLine(string.Format("{0} {1}", p.Name, p.TelephoneNumber.Number));
            }
        }

        static void PrintPhrases(TelephoneGameRepository repository)
        {
            foreach (GamePhrase p in GameService.GetPhraseList(repository))
            {
                Console.WriteLine(string.Format("{0} {1}", p.id, p.text));
            }
        }

        static void Choices(Game game, TelephoneGameRepository repository)
        {
            bool success = false;
            while (!success)
            {
                int choice = -1;
                while (-1 == choice)
                {
                    foreach (EdgeConditional edge in game.Edges)
                    {
                        Console.WriteLine(string.Format("{0}: {1}", edge.id, edge.text));
                    }
                    Console.WriteLine("Choose: ");
                    int.TryParse(Console.ReadLine(), out choice);
                }
                EdgeConditional chosenEdge = game.Edges.First<EdgeConditional>(e => e.id.Equals(choice));
                success = GameService.TransitionGameState(game, chosenEdge, repository);
                if (!success) Console.WriteLine("Could not transition state");
            }
        }
    }
}
