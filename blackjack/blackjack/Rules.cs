using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackjack
{
    class Rules
    {
        public Rules()
        {

        }

        /*
         * A player may only split their hand once
         * A player may only split their hand if they have only 2 cards
         * A player may only split their hand if the value of both cards is equal in their hand
         */
        public bool checkSplit(Player player)
        {
            bool returnValue = false;

            if (!player.didSplit())
            {
                if (player.getHand().getCards().Length == 2)
                {
                    if (player.getHand().getCardValues()[0] == player.getHand().getCardValues()[1])
                    {
                        Console.WriteLine("card 1 : " + player.getHand().getCardValues()[0]);
                        Console.WriteLine("card 2 : " + player.getHand().getCardValues()[1]);
                        returnValue = true;
                    }
                }
            }

            return returnValue;
        }

        /*
         * a player may only double down once on a hand (if they have split their hand they may double down on each hand)
         */
        public bool checkDoubleDown(Player player)
        {
            bool returnValue = false;

            if (player.didSplit())
            {
                int currentHand = player.getCurrentHand();
                if (currentHand == 0)
                {
                    if (!player.didDoubleDownHand())
                    {
                        if (player.getHand().getCards().Length == 2)
                        {
                            returnValue = true;
                        }
                    }
                }
                else if (currentHand == 1)
                {
                    if (!player.didDoubleDownSplitHand())
                    {
                        if (player.getSplitHand().getCards().Length == 2)
                        {
                            returnValue = true;
                        }
                    }
                }
            }
            else if (!player.didSplit())
            {
                if (!player.didDoubleDownHand())
                {
                    if (player.getHand().getCards().Length == 2)
                    {
                        returnValue = true;
                    }
                }
            }

            return returnValue;
        }

        /*
         * a player may only hit if they have not decided to stand
         * a player may only hit if their handValue is less than 21
         * after doubling down a player may only draw one card
         */
        public bool checkHit(Player player)
        {
            bool returnValue = false;
            int currentHand = player.getCurrentHand();

            if (!player.didStandHand())
            {
                if (currentHand == 0)
                {
                    if (!player.didDoubleDownHand())
                    {
                        if (player.getHand().getHandValue() < 21)
                        {
                            returnValue = true;
                        }
                    }
                }
            }
            if (!player.didStandSplitHand())
            {
                if (currentHand == 1)
                {
                    if (!player.didDoubleDownSplitHand())
                    {
                        if (player.getSplitHand().getHandValue() < 21)
                        {
                            returnValue = true;
                        }
                    }
                }
            }
            
            return returnValue;
        }

        public bool checkStand(Player player)
        {
            bool returnValue = false;
            int currentHand = player.getCurrentHand();

            if (currentHand == 0)
            {
                if (!player.isHandBusted() || !player.didDoubleDownHand())
                {
                    returnValue = true;
                }
            }
            else if (currentHand == 1)
            {
                if (!player.isSplitHandBusted() || !player.didDoubleDownSplitHand())
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }

        public bool checkBustHand(Player player)
        {
            bool returnValue = false;

            if (player.getHand().getHandValue() > 21)
            {
                returnValue = true;
            }

            return returnValue;
        }

        public bool checkBustSplitHand(Player player)
        {
            bool returnValue = false;

            if (player.getSplitHand().getHandValue() > 21)
            {
                returnValue = true;
            }

            return returnValue;
        }

        public bool checkBustHand(Agent agent)
        {
            bool returnValue = false;

            if (agent.getHand().getHandValue() > 21)
            {
                returnValue = true;
            }

            return returnValue;
        }

        public bool checkHandOver(Player player, Dealer dealer)
        {
            bool returnValue = false;

            // if the players hands have busted or
            // if the dealer hand has busted or
            // if the player has called to stand, and the dealer has called to stand
            // award or penalize and end this round

            if (player.didSplit())
            {
                if (player.isHandBusted() && player.isSplitHandBusted())
                {
                    returnValue = true;
                }
            }
            else if (!player.didSplit())
            {
                if (player.isHandBusted())
                {
                    returnValue = true;
                }
            }

            if (!returnValue)
            {
                if (dealer.isHandBusted())
                {
                    returnValue = true;
                }
            }

            if (!returnValue)
            {
                if (player.didSplit())
                {
                    if ((player.didStandHand() || player.didStandSplitHand()) || dealer.didStandHand())
                    {
                        returnValue = true;
                    }
                }
                else if (!player.didSplit())
                {
                    if (player.didStandHand() || dealer.didStandHand())
                    {
                        returnValue = true;
                    }
                }
            }

            return returnValue;
        }

        public bool checkBlackJack(Hand hand)
        {
            bool returnValue = false;

                if (hand.getCards().Length == 2)
                {
                    if (hand.getHandValue() == 21)
                    {
                        returnValue = true;
                    }
                }
            return returnValue;
        }

        public void determineWinner(int handInPlay, Player player, Dealer dealer)
        {
            bool handOver = checkHandOver(player, dealer);

            if (handInPlay == 0)
            {
                // If the player didn't split their hand
                if (!player.didSplit())
                {
                    if (checkBlackJack(player.getHand()))
                    {
                        Console.WriteLine("You got a Black Jack!! Payout 3 : 1");
                        player.getBank().collectWinnings(player.getBank().getWager() * 3);
                    }
                    // if the player stood on their hand
                    else if (player.didStandHand())
                    {
                        // if the dealer stood on their hand
                        if (dealer.didStandHand())
                        {
                            // if the player beat the dealer
                            if (player.getHand().getHandValue() > dealer.getHand().getHandValue()
                                && player.getHand().getHandValue() <= 21
                                && dealer.getHand().getHandValue() < 21)
                            {
                                Console.WriteLine("Your hand has beaten the dealer, congratulations payout 2 : 1.");
                                player.getBank().collectWinnings(player.getBank().getWager() * 2);
                            }
                            // if the dealer beat the player
                            else if (player.getHand().getHandValue() < dealer.getHand().getHandValue()
                                && player.getHand().getHandValue() < 21
                                && dealer.getHand().getHandValue() <= 21)
                            {
                                Console.WriteLine("Your hand has lost to the dealer, you lost your bet.");
                            }
                            // if the player and the dealer tied on their hands
                            else if (player.getHand().getHandValue() == dealer.getHand().getHandValue()
                                && player.getHand().getHandValue() == 21)
                            {
                                if (dealer.getHand().getCards().Length == 2 && player.getHand().getCards().Length > 2)
                                {
                                    Console.WriteLine("Dealer has BlackJack! you lose your bet.");
                                }
                                else
                                {
                                    Console.WriteLine("Your hand has tied the dealer, you keep your bet.");
                                    player.getBank().returnWager(player.getBank().getWager());
                                }
                            }
                        }
                        // if the dealer busted on their hand
                        else if (dealer.isHandBusted())
                        {
                            Console.WriteLine("Your hand has beaten the dealer, congratulations payout 2 : 1.");
                            player.getBank().collectWinnings(player.getBank().getWager() * 2);
                        }
                    }
                    // if the player busted on their hand
                    else if (player.isHandBusted())
                    {
                        Console.WriteLine("Your hand has lost to the dealer, you lost your bet.");
                    }
                }
            }
            else if (handInPlay == 1)
            {
                // if the player did split their hand
                if (player.didSplit())
                {
                    if (checkBlackJack(player.getHand()))
                    {
                        Console.WriteLine("You got a Black Jack on your first hand!! Payout 3 : 1");
                        player.getBank().collectWinnings((player.getBank().getWager() / 2) * 3);
                    }
                    // if the player stood on their hand
                    else if (player.didStandHand())
                    {
                        // if the dealer stood on their hand
                        if (dealer.didStandHand())
                        {
                            // if the player beat the dealer
                            if (player.getHand().getHandValue() > dealer.getHand().getHandValue()
                                && player.getHand().getHandValue() <= 21
                                && dealer.getHand().getHandValue() < 21)
                            {
                                Console.WriteLine("Your hand has beaten the dealer, congratulations payout 2 : 1.");
                                player.getBank().collectWinnings(player.getBank().getWager());
                            }
                            // if the dealer beat the player
                            else if (player.getHand().getHandValue() < dealer.getHand().getHandValue()
                                && player.getHand().getHandValue() < 21
                                && dealer.getHand().getHandValue() <= 21)
                            {
                                Console.WriteLine("Your hand has lost to the dealer, you lost your bet.");
                            }
                            // if the player and the dealer tied on thier hands
                            else if (player.getHand().getHandValue() == dealer.getHand().getHandValue()
                                && player.getHand().getHandValue() == 21)
                            {
                                if (dealer.getHand().getCards().Length == 2 && player.getHand().getCards().Length > 2)
                                {
                                    Console.WriteLine("Dealer has BlackJack! you lose your bet.");
                                }
                                else
                                {
                                    Console.WriteLine("Your hand has tied the dealer, you keep your bet.");
                                    player.getBank().returnWager(player.getBank().getWager() / 2);
                                }
                            }
                        }
                        // if the dealer busted on their hand
                        else if (dealer.isHandBusted())
                        {
                            Console.WriteLine("Your hand has beaten the dealer, congratulations payout 2 : 1.");
                            player.getBank().collectWinnings(player.getBank().getWager());
                        }
                    }
                    // if the player busted on their hand
                    else if (player.isHandBusted())
                    {
                        Console.WriteLine("Your hand has lost to the dealer, you lost your bet.");
                    }

                    if (checkBlackJack(player.getSplitHand()))
                    {
                        Console.WriteLine("You got a Black Jack on your split hand!! Payout 3 : 1");
                        player.getBank().collectWinnings((player.getBank().getWager() / 2) * 3);
                    }
                    // if the player stood on their split hand
                    else if (player.didStandSplitHand())
                    {
                        // if the dealer stood on their hand
                        if (dealer.didStandHand())
                        {
                            // if the players split hand beat the dealer
                            if (player.getSplitHand().getHandValue() > dealer.getHand().getHandValue()
                                && player.getSplitHand().getHandValue() <= 21
                                && dealer.getHand().getHandValue() < 21)
                            {
                                Console.WriteLine("Your split hand has beaten the dealer, congratulations payout 2 : 1");
                                player.getBank().collectWinnings(player.getBank().getWager());
                            }
                            // if the dealer beat the player
                            else if (player.getSplitHand().getHandValue() < dealer.getHand().getHandValue()
                                && player.getSplitHand().getHandValue() < 21
                                && dealer.getHand().getHandValue() <= 21)
                            {
                                Console.WriteLine("Your hand has lost to the dealer, you lost your bet.");
                            }
                            // if the player and the dealer tied on their hands
                            else if (player.getSplitHand().getHandValue() == dealer.getHand().getHandValue())
                            {
                                if (dealer.getHand().getCards().Length == 2 && player.getSplitHand().getCards().Length > 2
                                && player.getSplitHand().getHandValue() == 21)
                                {
                                    Console.WriteLine("Dealer has BlackJack! you lose your bet.");
                                }
                                else
                                {
                                    Console.WriteLine("Your hand has tied the dealer, you keep your bet.");
                                    player.getBank().returnWager(player.getBank().getWager() / 2);
                                }
                            }
                        }
                        // if the dealer busted their hand
                        else if (dealer.isHandBusted())
                        {
                            Console.WriteLine("Your split hand has beaten the dealer, congratulations payout 2 : 1");
                            player.getBank().collectWinnings(player.getBank().getWager());
                        }
                    }
                    // if the player busted on their hand
                    else if (player.isSplitHandBusted())
                    {
                        Console.WriteLine("Your hand has lost to the dealer, you lost your bet.");
                    }
                }
            }
        }
    }
}
