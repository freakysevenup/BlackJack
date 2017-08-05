using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayingCards;

namespace blackjack
{
    class Human : Player
    {
        public Human()
        {
            m_bank = new Bank();
            m_hand = new Hand();
            m_currentHand = 0;
            m_standHand = false;
            m_doubleDownHand = false;
            m_doubleDownSplitHand = false;
            m_split = false;
            m_hitCounter = 0;
            m_bustedHand = false;
            m_splitHandBusted = false;
        }

        public override void hit(Card card, Hand hand)
        {
            hand.addCard(card);
        }

        public override void standHand()
        {
            m_standHand = true;
            m_currentHand = 1;
        }

        public override void split(Card handCard, Card splitHandCard)
        {
            m_split = true;
            m_splitHand = new Hand();

            m_splitHand.addCard(m_hand.removeSplitCard(m_hand.getCards()[1]));
            m_splitHand.addCard(splitHandCard);
            m_hand.addCard(handCard);
        }

        public override void doubleDownHand()
        {
            m_doubleDownHand = true;
        }

        public override void doubleDownSplitHand()
        {
            m_doubleDownSplitHand = true;
        }

        public override Hand getHand()
        {
            return m_hand;
        }

        public override Hand getSplitHand()
        {
            return m_splitHand;
        }

        public override int getCurrentHand()
        {
            return m_currentHand;
        }

        public override void setCurrentHand(int hand)
        {
            m_currentHand = hand;
        }

        public override Bank getBank()
        {
            return m_bank;
        }

        public override void standSplitHand()
        {
            m_standSplitHand = true;
        }

        public override bool didStandHand()
        {
            return m_standHand;
        }

        public override bool didDoubleDownHand()
        {
            return m_doubleDownHand;
        }

        public override bool didDoubleDownSplitHand()
        {
            return m_doubleDownSplitHand;
        }

        public override bool didSplit()
        {
            return m_split;
        }

        public override int getHitCounter()
        {
            return m_hitCounter;
        }

        public override bool isHandBusted()
        {
            return m_bustedHand;
        }

        public override bool isSplitHandBusted()
        {
            return m_splitHandBusted;
        }

        public override bool didStandSplitHand()
        {
            return m_standSplitHand;
        }

        public override void bustHand()
        {
            m_bustedHand = true;
        }

        public override void bustSplitHand()
        {
            m_splitHandBusted = true;
        }

        public override void endRound()
        {
            m_hand = new Hand();
            m_splitHand = new Hand();
            m_currentHand = 0;
            m_standHand = false;
            m_doubleDownHand = false;
            m_doubleDownSplitHand = false;
            m_split = false;
            m_hitCounter = 0;
            m_bustedHand = false;
            m_splitHandBusted = false;
        }
    }
}
