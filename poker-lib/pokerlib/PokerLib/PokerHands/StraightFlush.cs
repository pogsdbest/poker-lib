using System;
using System.Collections.Generic;
using System.Text;

namespace PokerLib.PokerHands {
    class StraightFlush : PokerHand {

        public StraightFlush () {
            rank = PokerHand.STRAIGHT_FLUSH;
            name = "Straight Flush";
        }

        public override bool IsValid( List<Card> cards ) {
            //check if all same suit
            if(!IsSameSuit ( cards )) {
                return false;
            }

            cards = SortCards ( cards );
            //check if all card is in order
            if(!IsStraight (cards)) {
                
                return false;
            }

            return true;
        }

        /*Defines Handling Tie Breaker for Straight Flush
         * By Checking Every playerHands with the same Straight card rank
         * the hand with the highest card will be the winner.
         */ 
        public override List<PlayerHand> ProcessTieBreaker( List<PlayerHand> playerHands ) {
            List<PlayerHand> winners = new List<PlayerHand> ();
            int highest = 0;
            for(int i=0;i<playerHands.Count;i++) {
                PlayerHand playerHand = playerHands[i];
                List<Card> cards = playerHand.cardsInHand;
                cards = SortCards ( cards );
                Card cardHighest = cards[cards.Count - 1];
                int handHighest = cardHighest.rank;
                playerHand.tieInfo = "High Card " + cardHighest.name;

                if(winners.Count == 0) {
                    highest = handHighest;
                    winners.Add ( playerHand );
                } else {
                    if(handHighest > highest) {
                        winners.Clear ();
                        highest = handHighest;
                        
                        winners.Add ( playerHand );
                    } else if(handHighest == highest) {
                        
                        winners.Add ( playerHand );
                    }
                }
            }

            return winners;
        }
    }
}
