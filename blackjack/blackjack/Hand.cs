using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayingCards;

namespace blackjack
{
    class Hand
    {
        private Card[] m_cards;
        private int[] m_cardValues;
        private int m_handValue;

        public Hand()
        {
            m_cards = new Card[]{ null, null };
            m_cardValues = new int[]{ 0, 0 };
            m_handValue = 0;
        }

        public Card[] getCards() { return m_cards; }
        public int[] getCardValues() { return m_cardValues; }

        public int getHandValue() 
        {
            for (int i = 0; i < m_cards.Length; i++)
            {
                if (m_cards[i] == null)
                {
                    break;
                }
                else
                {
                    m_handValue += determineCardValue(m_cards[i]);
                }
            }
            return m_handValue; 
        }

        public void addCard(Card card)
        {
            Card[] cards;
            int[] cardValues;
            // if the last element is not null resize the array
            if (m_cards[m_cards.Length - 1] != null)
            {
                cards = new Card[m_cards.Length + 1];
                cardValues = new int[m_cardValues.Length + 1];
                for (int i = 0; i < m_cards.Length; i++)
                {
                    cards[i] = m_cards[i];
                    cardValues[i] = m_cardValues[i];
                }
                cards[m_cards.Length] = card;
                cardValues[m_cardValues.Length] = determineCardValue(card);
                m_cards = new Card[cards.Length];
                m_cardValues = new int[cardValues.Length];
                for (int i = 0; i < cards.Length; i++)
                {
                    m_cards[i] = cards[i];
                    m_cardValues[i] = cardValues[i];
                }
            }
            // Otherwise find the first element that is null and put the card in it
            else
            {
                for (int i = 0; i < m_cards.Length; i++)
                {
                    if (m_cards[i] == null)
                    {
                        m_cards[i] = card;
                        m_cardValues[i] = determineCardValue(card);
                        break;
                    }
                }
            }
        }

        public Card removeSplitCard(Card card)
        {
            Card returnSplitCard = null;
            if (m_cards.Length == 2)
            {
                returnSplitCard = m_cards[1];
                m_cards[1] = null;
            }

            return returnSplitCard;
        }

        private int determineCardValue(Card card)
        {
            int returnValue = 0;
            switch(card.getCardValue())
            {
                case CardValue.A:
                    if (m_handValue <= 10)
                    {
                        returnValue = 11;
                    }
                    else
                    {
                        returnValue = 1;
                    }
                    break;
                case CardValue.TWO:
                    returnValue = 2;
                    break;
                case CardValue.THREE:
                    returnValue = 3;
                    break;
                case CardValue.FOUR:
                    returnValue = 4;
                    break;
                case CardValue.FIVE:
                    returnValue = 5;
                    break;
                case CardValue.SIX:
                    returnValue = 6;
                    break;
                case CardValue.SEVEN:
                    returnValue = 7;
                    break;
                case CardValue.EIGHT:
                    returnValue = 8;
                    break;
                case CardValue.NINE:
                    returnValue = 9;
                    break;
                case CardValue.TEN:
                    returnValue = 10;
                    break;
                case CardValue.J:
                    returnValue = 10;
                    break;
                case CardValue.Q:
                    returnValue = 10;
                    break;
                case CardValue.K:
                    returnValue = 10;
                    break;
            }

            return returnValue;
        }

    }
}
