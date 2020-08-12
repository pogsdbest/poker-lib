using System;
using System.Collections.Generic;
using System.Text;

namespace PokerLib.PokerHands {
    class RoyalFlush : PokerHand {

        public RoyalFlush() {
            rank = PokerHand.ROYAL_FLUSH;
            name = "Royal Flush";
        }

        public override bool IsValid( List<Card> cards ) {
            //check if all same suit
            if(!IsSameSuit(cards)) {
                return false;
            }
            //check if 10,J,Q,K,A is present
            if( !Contains(cards,10) || !Contains ( cards, Card.JACK ) || !Contains ( cards, Card.QUEEN ) 
                || !Contains ( cards, Card.KING) || !Contains ( cards, Card.ACE ) ) {
                
                return false;
            }

            return true;
        }

        /*Everyone Won When Getting Royal Flush
         * 
         */
        public override List<PlayerHand> ProcessTieBreaker( List<PlayerHand> playerHands ) {
            List<PlayerHand> winners = new List<PlayerHand> ();
            
            for(int i = 0; i < playerHands.Count; i++) {
                PlayerHand playerHand = playerHands[i];

                Card card = GetCard (playerHand.cardsInHand , Card.ACE);
                playerHand.tieInfo = "High Card "+card.name;
                winners.Add (playerHand);
            }

            return winners;
        }
    }
}
