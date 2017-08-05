using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayingCards;

namespace blackjack
{
    abstract class Player : Agent
    {
        protected Bank m_bank;
        protected bool m_doubleDownSplitHand;
        protected int m_currentHand;
        protected Hand m_splitHand;
        protected bool m_split;
        protected bool m_doubleDownHand;
        protected bool m_splitHandBusted;
        protected bool m_standSplitHand;

        public Player() { }

        public abstract Bank getBank();
        public abstract void split(Card handCard, Card splitHandCard);
        public abstract void doubleDownHand();
        public abstract void doubleDownSplitHand();
        public abstract bool didSplit();
        public abstract bool didDoubleDownSplitHand();
        public abstract bool didDoubleDownHand();
        public abstract Hand getSplitHand();
        public abstract void setCurrentHand(int hand);
        public abstract int getCurrentHand();
        public abstract bool isSplitHandBusted();
        public abstract void standSplitHand();
        public abstract bool didStandSplitHand();
        public abstract void bustSplitHand();

        public override void hit(Card card, Hand hand)
        {

        }

        public override void standHand()
        {

        }

        public override Hand getHand()
        {
            return m_hand;
        }

        public override bool didStandHand()
        {
            return m_standHand;
        }

        public override int getHitCounter()
        {
            return m_hitCounter;
        }

        public override bool isHandBusted()
        {
            return m_bustedHand;
        }

        public override void bustHand()
        {
            m_bustedHand = true;
        }

        public override void endRound()
        {

        }


    }
}
