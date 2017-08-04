using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackjack
{

    class Bank
    {
        private int m_cashFlow;
        private int m_wager;
        private const int MIN = 5;
        private const int MAX = 100;

        public Bank()
        {
            m_cashFlow = 500;
            m_wager = 0;
        }

        public int getCashFlow() { return m_cashFlow; }

        public void setWager(int wager)
        {
            m_wager = wager;
        }

        public int getWager() { return m_wager; }

        public void betMin()
        {
            m_wager = MIN;
        }

        public void betMax()
        {
            m_wager = MAX;
        }

        public void deductWager()
        {
            m_cashFlow -= m_wager;
        }

        public void collectWinnings(int winnings)
        {
            m_cashFlow += winnings;
        }

        public void buyIn(int cash)
        {
            if (m_cashFlow >= 0)
            {
                m_cashFlow = cash;
            }
            else
            {
                m_cashFlow += cash;
            }
        }
    }
}
