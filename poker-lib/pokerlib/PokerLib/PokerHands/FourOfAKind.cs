using System;
using System.Collections.Generic;
using System.Text;

namespace PokerLib.PokerHands {
    class FourOfAKind : PokerHand {

        public FourOfAKind () {
            rank = PokerHand.FOUR_OF_A_KIND;
            name = "Four Of A Kind";
        }

        public override bool IsValid( List<Card> cards ) {
            
            bool isFourOfAKind = false;
            //Look for a possible 4 cards with same rank
            for(int i=0;i<cards.Count;i++) {
                List<Card> sameRankCards = GetSameRank ( cards[i].rank, cards );

                if(sameRankCards.Count >= 4) {
                    isFourOfAKind = true;
                    break;
                }
            }

            if(!isFourOfAKind) {
                return false;
            }

            return true;
        }

        /*Defines Handling of Four Of A Kind Tie Breaker. Ace Beats any other 4OfAKind
         * Combination
         */
        public override List<PlayerHand> ProcessTieBreaker( List<PlayerHand> playerHands ) {
            List<PlayerHand> winners = new List<PlayerHand> ();
            int highestPair = 0;
            for(int i = 0; i < playerHands.Count; i++) {
                PlayerHand playerHand = playerHands[i];
                List<Card> cards = playerHand.cardsInHand;
                int handHighestPair = 0;

                for(int j = 0; j < cards.Count; j++) {
                    List<Card> sameRankCards = GetSameRank ( cards[j].rank, cards );
                    if(sameRankCards.Count >= 4) {
                        Card card = sameRankCards[0];
                        handHighestPair = card.rank;
                        if(handHighestPair == Card.ACE) {
                            handHighestPair = Card.ACE_HIGH;
                            playerHand.tieInfo = "4 ACE "+ GetNames ( sameRankCards );
                        } else {
                            playerHand.tieInfo = "4 Of A Kind "+ GetNames ( sameRankCards );
                        }
                        break;
                    }
                }

                if(winners.Count == 0) {
                    highestPair = handHighestPair;
                    winners.Add ( playerHand );
                } else {
                    if(handHighestPair > highestPair ) {
                        winners.Clear ();
                        highestPair = handHighestPair;

                        winners.Add ( playerHand );
                    }
                }
            }

            return winners;
        }


    }
}
