using System;
using System.Collections.Generic;
using System.Text;

namespace PokerLib.PokerHands {
    class ThreeOfAKind : PokerHand {

        public ThreeOfAKind() {
            rank = PokerHand.THREE_OF_A_KIND;
            name = "Three Of A Kind";
        }

        public override bool IsValid( List<Card> cards ) {

            bool isThreeOfAKind = false;
            //Look for a possible 4 cards with same rank
            for(int i = 0; i < cards.Count; i++) {
                List<Card> sameRankCards = GetSameRank ( cards[i].rank, cards );

                if(sameRankCards.Count >= 3) {
                    isThreeOfAKind = true;
                }
            }

            if(!isThreeOfAKind) {
                return false;
            }

            return true;
        }

        public override List<PlayerHand> ProcessTieBreaker( List<PlayerHand> playerHands ) {
            List<PlayerHand> winners = new List<PlayerHand> ();
            int highest3OfAKind = 0;
            for(int i = 0; i < playerHands.Count; i++) {
                PlayerHand playerHand = playerHands[i];
                List<Card> cards = playerHand.cardsInHand;
                int handHighestPair = 0;

                for(int j = 0; j < cards.Count; j++) {
                    List<Card> sameRankCards = GetSameRank ( cards[j].rank, cards );
                    if(sameRankCards.Count >= 3) {
                        Card card = sameRankCards[0];
                        handHighestPair = card.rank;
                        if(handHighestPair == Card.ACE) {
                            handHighestPair = Card.ACE_HIGH;
                            playerHand.tieInfo = "3 ACE " + GetNames ( sameRankCards );
                        } else {
                            playerHand.tieInfo = "3 Of A Kind High Card " + GetNames ( sameRankCards );

                        }
                        break;
                    }
                }

                if(winners.Count == 0) {
                    highest3OfAKind = handHighestPair;
                    winners.Add ( playerHand );
                } else {
                    if(handHighestPair > highest3OfAKind) {
                        winners.Clear ();
                        highest3OfAKind = handHighestPair;

                        winners.Add ( playerHand );
                    }
                }

            }
            return winners;
        }
    }
}
