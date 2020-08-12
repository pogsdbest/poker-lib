using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PokerLib.PokerHands {
    public class PokerHand {
        public const int ROYAL_FLUSH = 10;
        public const int STRAIGHT_FLUSH = 9;
        public const int FOUR_OF_A_KIND = 8;
        public const int FULL_HOUSE = 7;
        public const int FLUSH = 6;
        public const int STRAIGHT = 5;
        public const int THREE_OF_A_KIND = 4;
        public const int TWO_PAIR = 3;
        public const int ONE_PAIR = 2;
        public const int HIGH_CARD = 1;

        public int rank;
        public string name;
        public int[] highCards;

        /*Different Validation Procedure for Every Poker Hands
         * 
         */
        public virtual bool IsValid(List<Card> cards) {
            return true;
        }

        /*Different Tie Breaker Procedure for Every Poker Hands
         * 
         */
        public virtual List<PlayerHand> ProcessTieBreaker(List<PlayerHand> playerHands) {
            return new List<PlayerHand> ();
        }

        public bool IsSameSuit( List<Card> cards) {
            int suit = cards[0].suit;
            for(int i=1;i<cards.Count;i++) {
                Card card = cards[i];
                if(suit != card.suit)
                    return false;
            }

            return true;
        }

        public bool Contains( List<Card> cards , int cardRank) {
            for(int i = 0; i < cards.Count; i++) {
                Card card = cards[i];
                if(card.rank == cardRank)
                    return true;
            }
            return false;
        }

        public Card GetCard( List<Card> cards, int cardRank ) {
            for(int i = 0; i < cards.Count; i++) {
                Card card = cards[i];
                if(card.rank == cardRank)
                    return card;
            }
            return null;
        }

        public List<Card> SortCards( List<Card> cards ) {
            Card[] temp = cards.ToArray ();
            
            Card cardTemp = null;
            for(int i = 0; i < temp.Length; i++) {
                for(int j = i + 1; j < temp.Length; j++) {
                    if(temp[i].rank > temp[j].rank) {
                        cardTemp = temp[i];
                        temp[i] = temp[j];
                        temp[j] = cardTemp;
                    }
                }
            }
            return cards = new List<Card> ( temp );
            
        }

        public bool IsStraight( List<Card> cards ) {
            SortCards (cards);
            int currentNum = cards[0].rank;
            for(int i=1;i<cards.Count;i++) {
                
                if(cards[i].rank == currentNum + 1) {
                    currentNum = currentNum + 1;
                } else {
                    return false;
                }
            }
            return true;
        }

        public List<Card> GetSameRank(int cardRank,List<Card> cards) {
            List<Card> sameRankCards = new List<Card> ();
            for(int i=0;i<cards.Count;i++) {
                Card card = cards[i];
                if(card.rank == cardRank) {
                    sameRankCards.Add (card);
                }
            }
            return sameRankCards;
        }

        public List<Card> GetOtherRank(int cardRank, List<Card> cards) {
            List<Card> otherRankCards = new List<Card> ();
            for(int i = 0; i < cards.Count; i++) {
                Card card = cards[i];
                if(card.rank != cardRank) {
                    otherRankCards.Add ( card );
                }
            }
            return otherRankCards;
        }

        public List<List<Card>> GetPairs(List<Card> cards) {
            List<List<Card>> pairs = new List<List<Card>> ();
            for(int i = 0; i < cards.Count; i++) {
                Card card1 = cards[i];
                for(int j = i + 1; j < cards.Count; j++) {
                    Card card2 = cards[j];
                    if(j < cards.Count) {
                        if(card1.rank == card2.rank) {
                            List<Card> pair = new List<Card> ( 2 );
                            pair.Add ( card1 );
                            pair.Add ( card2 );
                            pairs.Add (pair);
                        }
                    }
                }
            }

            return pairs;
        }

        public string GetNames(List<Card> cards) {
            string names = cards[0].name;
            for(int i = 1; i < cards.Count; i++) {
                names += "," + cards[i].name;
            }
            return names;
        }

        public bool LeftPairHigher( List<Card> leftPair, List<Card> rightPair ) {
            int leftPairRank = leftPair[0].rank;
            if(leftPairRank == Card.ACE)
                leftPairRank = Card.ACE_HIGH;

            int rightPairRank = rightPair[0].rank;
            if(rightPairRank == Card.ACE)
                rightPairRank = Card.ACE_HIGH;

            if(leftPairRank > rightPairRank) {
                return true;
            }
            return false;
        }

        public bool EqualPair( List<Card> leftPair, List<Card> rightPair ) {
            int leftPairRank = leftPair[0].rank;
            if(leftPairRank == Card.ACE)
                leftPairRank = Card.ACE_HIGH;

            int rightPairRank = rightPair[0].rank;
            if(rightPairRank == Card.ACE)
                rightPairRank = Card.ACE_HIGH;

            if(leftPairRank == rightPairRank) {
                return true;
            }
            return false;
        }

        public List<Card> ChangeAceToHighAce(List<Card> cards) {
            for(int i=0;i<cards.Count;i++) {
                Card card = cards[i];
                if(card.rank == Card.ACE) {
                    card.rank = Card.ACE_HIGH;
                }
            }
            return cards;
        }
    }
}
