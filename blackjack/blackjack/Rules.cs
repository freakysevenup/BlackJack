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
    }
}
