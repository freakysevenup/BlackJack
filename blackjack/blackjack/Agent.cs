using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayingCards;

namespace blackjack
{

    abstract class Agent
    {
        protected Hand m_hand;
        protected bool m_standHand;
        protected int m_hitCounter;
        protected bool m_bustedHand;

        public Agent() { }

        public abstract void hit(Card card, Hand hand);
        public abstract void standHand();
        public abstract Hand getHand();
        public abstract bool didStandHand();
        public abstract int getHitCounter();
        public abstract bool isHandBusted();

    }
}
