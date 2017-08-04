using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayingCards;

namespace blackjack
{
    class Dealer : Agent
    {
        public Dealer()
        {
            m_hand = new Hand();
            m_stand = false;
            m_doubleDown = false;
            m_split = false;
            m_hitCounter = 0;
        }

        public override void hit(Card card)
        {
            m_hand.addCard(card);
        }

        public override void stand()
        {
            m_stand = true;
        }

        public override void split()
        {
            m_split = true;
            m_splitHand = new Hand();
        }

        public override void doubleDown()
        {
            m_doubleDown = true;
        }

        public override Hand getHand()
        {
            return m_hand;
        }

        public override Hand getSplitHand()
        {
            return m_splitHand;
        }

        public override bool didStand()
        {
            return m_stand;
        }

        public override bool didDoubleDown()
        {
            return m_doubleDown;
        }

        public override bool didSplit()
        {
            return m_split;
        }

        public override int getHitCounter()
        {
            return m_hitCounter;
        }
    }
}
