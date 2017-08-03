using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackjack
{

    abstract class Agent
    {
        Hand m_hand;
        Hand m_splitHand;

        public Agent()
        {

        }

        public abstract void hit();
        public abstract void stand();
        public abstract void split();
        public abstract void doubleDown();

    }
}
