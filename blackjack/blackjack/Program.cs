using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayingCards;

namespace blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            Console.WriteLine("******************************************************");
            Console.WriteLine("Grab a new deck of cards...");
            Console.WriteLine("******************************************************");
            for (int i = 0; i < deck.getDeck().Length; i++)
            {
                Console.WriteLine(deck.getDeck()[i].ToString());
            }
            Console.WriteLine("\n******************************************************");
            Console.WriteLine("Shuffle that deck of cards...");
            Console.WriteLine("******************************************************");
            deck.Shuffle();

            for (int i = 0; i < deck.getDeck().Length; i++)
            {
                Console.WriteLine(deck.getDeck()[i].ToString());
            }

            Console.WriteLine("\n******************************************************");
            Console.WriteLine("Draw the top card from the deck...");
            Console.WriteLine("******************************************************");
            Console.WriteLine(deck.drawCard().ToString());
            Console.WriteLine("\n******************************************************");
            Console.WriteLine("Draw the top card from the deck...");
            Console.WriteLine("******************************************************");
            Console.WriteLine(deck.drawCard().ToString());
            Console.WriteLine("\n******************************************************");
            Console.WriteLine("Draw the top card from the deck...");
            Console.WriteLine("******************************************************");
            Console.WriteLine(deck.drawCard().ToString());
            Console.WriteLine("\n******************************************************");
            Console.WriteLine("Draw the top card from the deck...");
            Console.WriteLine("******************************************************");
            Console.WriteLine(deck.drawCard().ToString());
            Console.WriteLine("\n******************************************************");
            Console.WriteLine("Draw the top card from the deck...");
            Console.WriteLine("******************************************************");
            Console.WriteLine(deck.drawCard().ToString());

            Console.ReadLine();
        }
    }
}
