using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayingCards;

namespace blackjack
{
    enum GameState
    {
        RUN,
        PLAYER_TURN,
        DEALER_TURN,
        EXIT
    }

    class Game
    {
        Deck m_deck;
        Player[] m_players;
        Dealer m_dealer;
        Rules m_rules;
        GameState m_gameState;
        bool m_dealtHand;

        public Game()
        {
            m_gameState = GameState.RUN;
        }

        public void run()
        {
            initialize();
        }

        private void initialize()
        {
            // set up game here
            m_dealtHand = false;
            m_rules = new Rules();
            m_deck = new Deck();
            m_players = new Player[1];

            m_players[0] = new Human();
            m_dealer = new Dealer();

            update();
        }

        private void deal()
        {
            for (int i = 0; i < 2; i++)
            {
                m_players[0].getHand().addCard(m_deck.drawCard());
                m_dealer.getHand().addCard(m_deck.drawCard());
            }

            m_dealtHand = true;
        }

        private void playerTurn()
        {
            m_gameState = GameState.PLAYER_TURN;
            while (m_gameState == GameState.PLAYER_TURN)
            {
                // Show the player their hand(s)
                // Show the player the dealers hand
                // Give the player their options
                // apply their choices
                // if they split and the first hand busts or they stand on it, start the second hand
                // when the player has either busted their hand(s) or decided to stand on their hand(s) leave this while loop

                if (!m_players[0].didSplit())
                {
                    Console.WriteLine("***** Player Hand *****");
                    for (int i = 0; i < m_players[0].getHand().getCards().Length; i++)
                    {
                        Console.Write(m_players[0].getHand().getCards()[i].ToString() + " ");
                    }
                    Console.WriteLine("\n***********************");

                    Console.WriteLine("***** Dealer Hand *****");
                    for (int i = 0; i < m_dealer.getHand().getCards().Length; i++)
                    {
                        Console.Write(m_dealer.getHand().getCards()[i].ToString() + " ");
                    }
                    Console.WriteLine("\n***********************");

                    Console.WriteLine(determineOptions());
                    acceptPlayerChoice(Console.ReadLine());
                }
                else
                {
                    Console.WriteLine("***** Player Hand *****");
                    for (int i = 0; i < m_players[0].getHand().getCards().Length; i++)
                    {
                        Console.Write(m_players[0].getHand().getCards()[i].ToString() + " ");
                    }
                    Console.WriteLine("\n***********************");

                    Console.WriteLine("***** Player Split Hand *****");
                    for (int i = 0; i < m_players[0].getSplitHand().getCards().Length; i++)
                    {
                        Console.Write(m_players[0].getSplitHand().getCards()[i].ToString() + " ");
                    }
                    Console.WriteLine("\n***********************");

                    Console.WriteLine("***** Dealer Hand *****");
                    for (int i = 0; i < m_dealer.getHand().getCards().Length; i++)
                    {
                        Console.Write(m_dealer.getHand().getCards()[i].ToString() + " ");
                    }
                    Console.WriteLine("\n***********************");

                    Console.WriteLine(determineOptions());
                    acceptPlayerChoice(Console.ReadLine());
                }
            }
        }

        private void dealerTurn()
        {
            m_gameState = GameState.DEALER_TURN;
            while (m_gameState == GameState.DEALER_TURN)
            {
                // Play out the dealers hand by the following rules
                // if the dealer has 17 points or more at any time they must stand
                // if the dealer has less than 17 points they may draw a card
                // if the dealer busts or stands leave this loop
            }
        }

        private string determineOptions()
        {
            string output = "Stand";
            if (m_rules.checkHit(m_players[0]))
            {
                output += " | Hit";
            }
            if (m_rules.checkDoubleDown(m_players[0]))
            {
                output += " | Double Down";
            }
            if (m_rules.checkSplit(m_players[0]))
            {
                output += " | Split";
            }

            string[] temp = output.Split('|');
            output = "";

            for (int i = 0; i < temp.Length; i++)
            {
                output += " ( " + (i + 1) + " ) " + temp[i] + " |";
            }
            output.TrimEnd('|');

            return output;
        }

        private void acceptPlayerChoice(string choice)
        {
            int currentHand = m_players[0].getCurrentHand();
            switch (choice)
            {
                case "1":
                    if (currentHand == 0)
                    {
                        m_players[0].standHand();
                    }
                    else if (currentHand == 1)
                    {
                        m_players[0].standSplitHand();
                    }
                    break;
                case "2":
                    if (currentHand == 0)
                    {
                        m_players[0].getHand().addCard(m_deck.drawCard());
                    }
                    else if (currentHand == 1)
                    {
                        m_players[0].getSplitHand().addCard(m_deck.drawCard());
                    }
                    break;
                case "3":
                    if (currentHand == 0)
                    {
                        m_players[0].doubleDownHand();
                    }
                    else if (currentHand == 1)
                    {
                        m_players[0].doubleDownSplitHand();
                    }
                    break;
                case "4":
                    m_players[0].split();
                    break;
            }
        }

        private void determineWinner()
        {

        }

        private void update()
        {
            while (m_gameState != GameState.EXIT)
            {
                if (!m_dealtHand)
                {
                    deal();
                }
                playerTurn();

                dealerTurn();

                determineWinner();

                string e = Console.ReadLine();
                if (e != string.Empty)
                {
                    m_gameState = GameState.EXIT;
                }
            }
        }
    }
}
