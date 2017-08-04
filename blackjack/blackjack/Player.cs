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

        public Player() { }

        public abstract Bank getBank();

        public override void hit(Card card)
        {

        }

        public override void stand()
        {

        }

        public override void split()
        {

        }

        public override void doubleDown()
        {

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
