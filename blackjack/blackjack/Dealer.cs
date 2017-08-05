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
            m_standHand = false;
            m_hitCounter = 0;
            m_bustedHand = false;
        }

        public override void hit(Card card, Hand hand)
        {
            m_hand.addCard(card);
        }

        public override void standHand()
        {
            m_standHand = true;
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
            m_hand = new Hand();
            m_standHand = false;
            m_hitCounter = 0;
            m_bustedHand = false;
        }
    }
}
