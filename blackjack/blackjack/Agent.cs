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
        protected Hand m_splitHand;
        protected bool m_stand;
        protected bool m_doubleDown;
        protected bool m_split;
        protected int m_hitCounter;

        public Agent() { }

        public abstract void hit(Card card);
        public abstract void stand();
        public abstract void split();
        public abstract void doubleDown();

        public abstract Hand getHand();
        public abstract Hand getSplitHand();
        public abstract bool didStand();
        public abstract bool didDoubleDown();
        public abstract bool didSplit();
        public abstract int getHitCounter();

    }
}
