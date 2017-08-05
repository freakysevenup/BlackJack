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
        NEXT_ROUND,
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
            Console.Title = "Black Jack by Jesse Derochie";

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
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Blue;

                if (m_handInPlay == 0)
                {
                    Console.WriteLine("Currently playing first hand");
                    Console.WriteLine("Current Hand value is : " + m_players[0].getHand().getHandValue());
                }
                else if (m_handInPlay == 1)
                {
                    Console.WriteLine("Currently playing split hand");
                    Console.WriteLine("Current Hand value is : " + m_players[0].getSplitHand().getHandValue());
                }

                Console.WriteLine("Current wager is : " + m_players[0].getBank().getWager());
                Console.WriteLine("Cash in chips : " + m_players[0].getBank().getCashFlow());
                Console.WriteLine();

                Console.ResetColor();

                if (!m_players[0].didSplit() && m_handInPlay == 0)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("***** Player Hand *****");
                    Console.ResetColor();
                    for (int i = 0; i < m_players[0].getHand().getCards().Length; i++)
                    {
                        Console.Write(m_players[0].getHand().getCards()[i].ToString() + " ");
                    }
                    Console.WriteLine("\n***********************");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("***** Dealer Hand *****");
                    Console.ResetColor();
                    for (int i = 0; i < m_dealer.getHand().getCards().Length; i++)
                    {
                        Console.Write(m_dealer.getHand().getCards()[i].ToString() + " ");
                    }
                    Console.WriteLine("\n***********************");

                    Console.WriteLine(determineOptions());
                    acceptPlayerChoice(Console.ReadLine());
                }
                else if (m_players[0].didSplit() && m_handInPlay == 0)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("***** Player Hand *****");
                    Console.ResetColor();
                    for (int i = 0; i < m_players[0].getHand().getCards().Length; i++)
                    {
                        Console.Write(m_players[0].getHand().getCards()[i].ToString() + " ");
                    }
                    Console.WriteLine("\n***********************");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("***** Player Split Hand *****");
                    Console.ResetColor();
                    for (int i = 0; i < m_players[0].getSplitHand().getCards().Length; i++)
                    {
                        Console.Write(m_players[0].getSplitHand().getCards()[i].ToString() + " ");
                    }
                    Console.WriteLine("\n***********************");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("***** Dealer Hand *****");
                    Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("***** Player Hand *****");
                    Console.ResetColor();
                    for (int i = 0; i < m_players[0].getHand().getCards().Length; i++)
                    {
                        Console.Write(m_players[0].getHand().getCards()[i].ToString() + " ");
                    }
                    Console.WriteLine("\n***********************");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("***** Player Split Hand *****");
                    Console.ResetColor();
                    for (int i = 0; i < m_players[0].getSplitHand().getCards().Length; i++)
                    {
                        Console.Write(m_players[0].getSplitHand().getCards()[i].ToString() + " ");
                    }
                    Console.WriteLine("\n***********************");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("***** Dealer Hand *****");
                    Console.ResetColor();
                    for (int i = 0; i < m_dealer.getHand().getCards().Length; i++)
                    {
                        Console.Write(m_dealer.getHand().getCards()[i].ToString() + " ");
                    }
                    Console.WriteLine("\n***********************");

                    Console.WriteLine(determineOptions());
                    acceptPlayerChoice(Console.ReadLine());

                }

                if (m_players[0].didSplit())
                {
                    if (m_handInPlay == 0)
                    {
                        if (m_rules.checkBustHand(m_players[0]) || m_players[0].didStandHand())
                        {
                            if (m_rules.checkBustHand(m_players[0]))
                            {
                                m_players[0].bustHand();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Your first hand has BUSTED!");
                                Console.ResetColor();
                            }
                            m_handInPlay = 1;
                        }
                    }

                    if (m_handInPlay == 1)
                    {
                        if (m_rules.checkBustSplitHand(m_players[0]) || m_players[0].didStandSplitHand())
                        {
                            if (m_rules.checkBustSplitHand(m_players[0]))
                            {
                                m_players[0].bustSplitHand();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Your split hand has BUSTED!");
                                Console.ResetColor();
                            }
                            m_gameState = GameState.RUN;
                        }
                    }
                }
                else if (!m_players[0].didSplit())
                {
                    if (m_rules.checkBustHand(m_players[0]) || m_players[0].didStandHand())
                    {
                        m_gameState = GameState.RUN;
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;

            if (!m_players[0].didSplit())
            {
                Console.WriteLine("Hand value is : " + m_players[0].getHand().getHandValue());
            }
            else if (m_players[0].didSplit())
            {
                Console.WriteLine("Hand value is : " + m_players[0].getHand().getHandValue());
                Console.WriteLine("Split Hand value is : " + m_players[0].getSplitHand().getHandValue());
            }

            Console.WriteLine("Wager is : " + m_players[0].getBank().getWager());
            Console.WriteLine("Cash in chips : " + m_players[0].getBank().getCashFlow());
            Console.WriteLine();

            Console.ResetColor();

            Console.WriteLine("Your turn is over, press enter to move to dealers turn.");
            Console.ReadLine();
            Console.Clear();
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
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Dealer Hand value is : " + m_dealer.getHand().getHandValue());
                Console.ResetColor();

                if (m_dealer.getHand().getHandValue() >= 17)
                {
                    m_dealer.standHand();
                    m_gameState = GameState.RUN;
                }
                else if (m_dealer.getHand().getHandValue() < 17)
                {
                    m_dealer.getHand().addCard(m_deck.drawCard());
                }

                if (m_dealer.getHand().getHandValue() > 21)
                {
                    m_dealer.bustHand();
                }

                Console.WriteLine("***** Dealer Hand *****");
                for (int i = 0; i < m_dealer.getHand().getCards().Length; i++)
                {
                    Console.Write(m_dealer.getHand().getCards()[i].ToString() + " ");
                }
                Console.WriteLine("\n***********************");
            }

            if (m_rules.checkBustHand(m_dealer) || m_dealer.didStandHand())
            {
                if (m_rules.checkBustHand(m_dealer))
                {
                    m_dealer.bustHand();
                }
                m_gameState = GameState.RUN;
            }

            Console.WriteLine("Dealer Turn has ended, press enter to see results.");
            Console.ReadLine();
            Console.Clear();
        }

        private string determineOptions()
        {
            string output = "( W ) Stand";
            if (m_rules.checkHit(m_players[0]))
            {
                output += " | ( A ) Hit";
            }
            if (m_rules.checkDoubleDown(m_players[0]))
            {
                output += " | ( S ) Double Down";
            }
            if (m_rules.checkSplit(m_players[0]))
            {
                output += " | ( D ) Split";
            }

            output += " | ( Q ) Quit";

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
                    m_players[0].getBank().betMin();
                    break;
                case "2":
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

            Console.Clear();
            return output;
        }

        private void acceptPlayerChoice(string choice)
        {
            int currentHand = m_players[0].getCurrentHand();
            switch (choice.ToLower())
            {
                case "w":
                    if (currentHand == 0)
                    {
                        m_players[0].standHand();
                    }
                    else if (currentHand == 1)
                    {
                        m_players[0].standSplitHand();
                    }
                    break;
                case "a":
                    if (currentHand == 0)
                    {
                        m_players[0].getHand().addCard(m_deck.drawCard());
                    }
                    else if (currentHand == 1)
                    {
                        m_players[0].getSplitHand().addCard(m_deck.drawCard());
                    }
                    break;
                case "s":
                    if (currentHand == 0)
                    {
                        m_players[0].doubleDownHand();
                        m_players[0].getHand().addCard(m_deck.drawCard());
                        m_players[0].getBank().doubleWager();
                    }
                    else if (currentHand == 1)
                    {
                        m_players[0].doubleDownSplitHand();
                        m_players[0].getSplitHand().addCard(m_deck.drawCard());
                        m_players[0].getBank().doubleWager();
                    }
                    break;
                case "d":
                    m_players[0].split(m_deck.drawCard(), m_deck.drawCard());
                    m_players[0].getBank().doubleWager();
                    break;
                case "q":
                    m_gameState = GameState.EXIT;
                    update();
                    break;
            }
            Console.Clear();
        }

        private void determineWinner(int handInPlay)
        {
            bool handOver = m_rules.checkHandOver(m_players[0], m_dealer);

            if (handInPlay == 0)
            {
                // If the player didn't split their hand
                if (!m_players[0].didSplit())
                {
                    // if the player stood on their hand
                    if (m_players[0].didStandHand())
                    {
                        // if the dealer stood on their hand
                        if (m_dealer.didStandHand())
                        {
                            // if the player beat the dealer
                            if (m_players[0].getHand().getHandValue() > m_dealer.getHand().getHandValue()
                                && m_players[0].getHand().getHandValue() <= 21
                                && m_dealer.getHand().getHandValue() < 21)
                            {
                                Console.WriteLine("Your hand has beaten the dealer, congratulations payout 2 : 1.");
                                m_players[0].getBank().collectWinnings(m_players[0].getBank().getWager() * 2);
                            }
                            // if the dealer beat the player
                            else if (m_players[0].getHand().getHandValue() < m_dealer.getHand().getHandValue()
                                && m_players[0].getHand().getHandValue() < 21
                                && m_dealer.getHand().getHandValue() <= 21)
                            {
                                Console.WriteLine("Your hand has lost to the dealer, you lost your bet.");
                            }
                            // if the player and the dealer tied on thier hands
                            else if (m_players[0].getHand().getHandValue() == m_dealer.getHand().getHandValue())
                            {
                                Console.WriteLine("Your hand has tied the dealer, you keep your bet.");
                                m_players[0].getBank().setWager(0);
                                m_players[0].getBank().returnWager(m_players[0].getBank().getWager());
                            }
                        }
                        // if the dealer busted on their hand
                        else if (m_dealer.isHandBusted())
                        {
                            Console.WriteLine("Your hand has beaten the dealer, congratulations payout 2 : 1.");
                            m_players[0].getBank().collectWinnings(m_players[0].getBank().getWager() * 2);
                        }
                    }
                    // if the player busted on their hand
                    else if ((m_players[0].isHandBusted() && m_dealer.isHandBusted())
                        || (m_players[0].isHandBusted() && !m_dealer.isHandBusted()))
                    {
                        Console.WriteLine("Your hand has lost to the dealer, you lost your bet.");
                    }
                }
            }
            else if (handInPlay == 1)
            {
                // if the player did split their hand
                if (m_players[0].didSplit())
                {
                    // if the player stood on their hand
                    if (m_players[0].didStandHand())
                    {
                        // if the dealer stood on their hand
                        if (m_dealer.didStandHand())
                        {
                            // if the player beat the dealer
                            if (m_players[0].getHand().getHandValue() > m_dealer.getHand().getHandValue()
                                && m_players[0].getHand().getHandValue() <= 21
                                && m_dealer.getHand().getHandValue() < 21)
                            {
                                Console.WriteLine("Your hand has beaten the dealer, congratulations payout 2 : 1.");
                                m_players[0].getBank().collectWinnings(m_players[0].getBank().getWager());
                            }
                            // if the dealer beat the player
                            else if (m_players[0].getHand().getHandValue() < m_dealer.getHand().getHandValue()
                                && m_players[0].getHand().getHandValue() < 21
                                && m_dealer.getHand().getHandValue() <= 21)
                            {
                                Console.WriteLine("Your hand has lost to the dealer, you lost your bet.");
                            }
                            // if the player and the dealer tied on thier hands
                            else if (m_players[0].getHand().getHandValue() == m_dealer.getHand().getHandValue())
                            {
                                Console.WriteLine("Your hand has tied the dealer, you keep your bet.");
                                m_players[0].getBank().setWager(0);
                                m_players[0].getBank().returnWager(m_players[0].getBank().getWager() / 2);
                            }
                        }
                        // if the dealer busted on their hand
                        else if (m_dealer.isHandBusted())
                        {
                            Console.WriteLine("Your hand has beaten the dealer, congratulations payout 2 : 1.");
                            m_players[0].getBank().collectWinnings(m_players[0].getBank().getWager());
                        }
                    }
                    // if the player busted on their hand
                    else if ((m_players[0].isHandBusted() && m_dealer.isHandBusted())
                        || (m_players[0].isHandBusted() && !m_dealer.isHandBusted()))
                    {
                        Console.WriteLine("Your hand has lost to the dealer, you lost your bet.");
                    }

                    // if the player stood on their split hand
                    if (m_players[0].didStandSplitHand())
                    {
                        // if the dealer stood on their hand
                        if (m_dealer.didStandHand())
                        {
                            // if the players split hand beat the dealer
                            if (m_players[0].getSplitHand().getHandValue() > m_dealer.getHand().getHandValue()
                                && m_players[0].getSplitHand().getHandValue() <= 21
                                && m_dealer.getHand().getHandValue() < 21)
                            {
                                Console.WriteLine("Your split hand has beaten the dealer, congratulations payout 2 : 1");
                                m_players[0].getBank().collectWinnings(m_players[0].getBank().getWager());
                            }
                            // if the dealer beat the player
                            else if (m_players[0].getSplitHand().getHandValue() < m_dealer.getHand().getHandValue()
                                && m_players[0].getSplitHand().getHandValue() < 21
                                && m_dealer.getHand().getHandValue() <= 21)
                            {
                                Console.WriteLine("Your hand has lost to the dealer, you lost your bet.");
                            }
                            // if the player and the dealer tied on their hands
                            else if (m_players[0].getSplitHand().getHandValue() == m_dealer.getHand().getHandValue())
                            {
                                Console.WriteLine("Your hand has tied the dealer, you keep your bet.");
                                m_players[0].getBank().setWager(0);
                                m_players[0].getBank().returnWager(m_players[0].getBank().getWager() / 2);
                            }
                        }
                        // if the dealer busted their hand
                        else if (m_dealer.isHandBusted())
                        {
                            Console.WriteLine("Your split hand has beaten the dealer, congratulations payout 2 : 1");
                            m_players[0].getBank().collectWinnings(m_players[0].getBank().getWager());
                        }
                    }
                    // if the player busted on their hand
                    else if ((m_players[0].isSplitHandBusted() && m_dealer.isHandBusted())
                        || (m_players[0].isSplitHandBusted() && !m_dealer.isHandBusted()))
                    {
                        Console.WriteLine("Your hand has lost to the dealer, you lost your bet.");
                    }
                }
            }
        }

        private void displayResults()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;

            if (m_players[0].didSplit())
            {
                Console.WriteLine("Hand value was : " + m_players[0].getHand().getHandValue());
                Console.WriteLine("Split Hand value was : " + m_players[0].getSplitHand().getHandValue());
            }
            else if (!m_players[0].didSplit())
            {
                Console.WriteLine("Hand value was : " + m_players[0].getHand().getHandValue());
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("Dealer Hand value was : " + m_dealer.getHand().getHandValue());
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("Current wager was : " + m_players[0].getBank().getWager());
            Console.WriteLine("Cash in chips : " + m_players[0].getBank().getCashFlow());
            Console.WriteLine();

            Console.ResetColor();

            m_gameState = GameState.NEXT_ROUND;
        }

        private void resetVariables()
        {
            m_dealtHand = false;
            m_handInPlay = 0;
            m_placedBet = false;
            m_players[0].endRound();
            m_dealer.endRound();

            if (m_deck.getDeck().Length > 20)
            {
                m_deck.Shuffle();
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

                if (m_players[0].didSplit())
                {
                    determineWinner(1);
                }
                else if (!m_players[0].didSplit())
                {
                    determineWinner(0);
                }

                displayResults();

                if (m_gameState == GameState.NEXT_ROUND)
                {
                    resetVariables();
                    m_gameState = GameState.RUN;
                }
            }

            if (m_gameState == GameState.EXIT)
            {
                Environment.Exit(0);
            }
        }
    }
}
