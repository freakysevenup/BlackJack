using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayingCards;

namespace blackjack
{
    class Game
    {
        Deck m_deck;
        Agent[] m_agents;


        public Game()
        {

        }

        public void run()
        {
            initialize();
        }

        private void initialize()
        {
            // set up game here

            m_deck = new Deck();

            m_agents = new Agent[2];

            m_agents[0] = new Dealer();
            m_agents[1] = new Human();

        }

    }
}
