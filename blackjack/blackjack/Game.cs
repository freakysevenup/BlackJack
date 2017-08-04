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
        int m_handInPlay;
        bool m_placedBet;

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
            m_handInPlay = 0;
            m_placedBet = false;

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

            if (!m_placedBet)
            {
                Console.WriteLine(betOptions());
                m_placedBet = true;
            }

            while (m_gameState == GameState.PLAYER_TURN)
            {
                // Show the player their hand(s)
                // Show the player the dealers hand
                // Give the player their options
                // apply their choices
                // if they split and the first hand busts or they stand on it, start the second hand
                // when the player has either busted their hand(s) or decided to stand on their hand(s) leave this while loop

                if (!m_players[0].didSplit() && m_handInPlay == 0)
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
                else if (m_players[0].didSplit() && m_handInPlay == 1)
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

                //if (m_rules.checkBustHand(m_players[0]) || m_players[0].didStandHand())
                //{
                //    m_handInPlay = 1;
                //}
                //if (m_players[0].didSplit())
                //{
                //    if (m_rules.checkBustSplitHand(m_players[0]) || m_players[0].didStandSplitHand())
                //    {
                //        m_gameState = GameState.RUN;
                //    }
                //}
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

                if (m_dealer.getHand().getHandValue() >= 17)
                {
                    m_dealer.standHand();
                    m_gameState = GameState.RUN;
                }
                else if (m_dealer.getHand().getHandValue() < 17)
                {
                    m_dealer.getHand().addCard(m_deck.drawCard());
                }
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

            output += " ( E ) Exit";

            return output;
        }

        private string betOptions()
        {
            string output = "";
            Console.WriteLine("Please place your bet.");
            Console.WriteLine("( 1 ) Min $5 | ( 2 ) Max $100");// | ( 3 ) Enter Other Amount");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    output = "You have bet the minimum of $5";
                    m_players[0].getBank().betMin();
                    break;
                case "2":
                    output = "You have bet the maximum of $100";
                    m_players[0].getBank().betMax();
                    break;
                //case "3":
                //    output = "Please enter an amount between $5 and $100";
                //    string response = Console.ReadLine();
                //    if (Convert.ToInt32(response) > 100 || Convert.ToInt32(response) < 5)
                //    {
                //        Console.WriteLine("The value you entered does not comply.");
                //        Console.WriteLine(betOptions());
                //    }
                //    else
                //    {
                //        m_players[0].getBank().setWager(Convert.ToInt32(response));
                //        output = "you have bet $" + response;
                //    }
                //    break;
            }
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
                case "e":
                    m_gameState = GameState.EXIT;
                    break;
            }
        }

        private void determineWinner()
        {
            bool handOver = m_rules.checkHandOver(m_players[0], m_dealer);

            // If the player didn't split their hand
            if (!m_players[0].didSplit())
            {
                if (m_players[0].didStandHand())
                {
                    if (m_dealer.didStandHand())
                    {
                        // if the player beat the dealer
                        if (m_players[0].getHand().getHandValue() > m_dealer.getHand().getHandValue())
                        {
                            Console.WriteLine("Your hand has beaten the dealer, congratulations payout 2 : 1.");
                            m_players[0].getBank().collectWinnings(m_players[0].getBank().getWager() * 2);
                        }
                        else if (m_players[0].getHand().getHandValue() < m_dealer.getHand().getHandValue())
                        {
                            Console.WriteLine("Your hand has lost to the dealer, you lost your bet.");
                            m_players[0].getBank().deductWager();
                        }
                        else if (m_players[0].getHand().getHandValue() == m_dealer.getHand().getHandValue())
                        {
                            Console.WriteLine("Your hand has tied the dealer, you keep your bet.");
                            m_players[0].getBank().setWager(0);
                        }
                    }
                    else if (m_dealer.isHandBusted())
                    {
                        Console.WriteLine("Your hand has beaten the dealer, congratulations payout 2 : 1.");
                        m_players[0].getBank().collectWinnings(m_players[0].getBank().getWager() * 2);
                    }
                }
                else if (m_players[0].isHandBusted())
                {
                    Console.WriteLine("Your hand has lost to the dealer, you lost your bet.");
                    m_players[0].getBank().deductWager();
                }
            }
            // if the player did split their hand
            else if (m_players[0].didSplit())
            {
                if (m_players[0].didStandHand())
                {
                    if (m_dealer.didStandHand())
                    {
                        // if the players first hand beat the dealer
                        if (m_players[0].getHand().getHandValue() > m_dealer.getHand().getHandValue())
                        {
                            Console.WriteLine("Your first hand has beaten the dealer, congratulations payout 2 : 1");
                            m_players[0].getBank().collectWinnings(m_players[0].getBank().getWager() * 2);
                        }
                        else if (m_players[0].getHand().getHandValue() < m_dealer.getHand().getHandValue())
                        {
                            Console.WriteLine("Your hand has lost to the dealer, you lost your bet.");
                            m_players[0].getBank().deductWager();
                        }
                        else if (m_players[0].getHand().getHandValue() == m_dealer.getHand().getHandValue())
                        {
                            Console.WriteLine("Your hand has tied the dealer, you keep your bet.");
                            m_players[0].getBank().setWager(0);
                        }
                    }
                    else if (m_dealer.isHandBusted())
                    {
                        Console.WriteLine("Your first hand has beaten the dealer, congratulations payout 2 : 1");
                        m_players[0].getBank().collectWinnings(m_players[0].getBank().getWager() * 2);
                    }
                }
                else if (m_players[0].isHandBusted())
                {
                    Console.WriteLine("Your hand has lost to the dealer, you lost your bet.");
                    m_players[0].getBank().deductWager();
                }


                if (m_players[0].didStandSplitHand())
                {
                    if (m_dealer.didStandHand())
                    {
                        // if the players split hand beat the dealer
                        if (m_players[0].getSplitHand().getHandValue() > m_dealer.getHand().getHandValue())
                        {
                            Console.WriteLine("Your split hand has beaten the dealer, congratulations payout 2 : 1");
                            m_players[0].getBank().collectWinnings(m_players[0].getBank().getWager() * 2);
                        }
                        else if (m_players[0].getSplitHand().getHandValue() < m_dealer.getHand().getHandValue())
                        {
                            Console.WriteLine("Your hand has lost to the dealer, you lost your bet.");
                            m_players[0].getBank().deductWager();
                        }
                        else if (m_players[0].getSplitHand().getHandValue() == m_dealer.getHand().getHandValue())
                        {
                            Console.WriteLine("Your hand has tied the dealer, you keep your bet.");
                            m_players[0].getBank().setWager(0);
                        }
                    }
                    else if (m_dealer.isHandBusted())
                    {
                        Console.WriteLine("Your split hand has beaten the dealer, congratulations payout 2 : 1");
                        m_players[0].getBank().collectWinnings(m_players[0].getBank().getWager() * 2);
                    }
                }
                else if (m_players[0].isSplitHandBusted())
                {
                    Console.WriteLine("Your hand has lost to the dealer, you lost your bet.");
                    m_players[0].getBank().deductWager();
                }
            }
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

                m_dealtHand = false;
                m_handInPlay = 0;
                m_placedBet = false;
            }
        }
    }
}
