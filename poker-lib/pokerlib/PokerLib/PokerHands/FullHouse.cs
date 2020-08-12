using System;
using System.Collections.Generic;
using System.Text;

namespace PokerLib.PokerHands {
    class FullHouse : PokerHand  {
        public FullHouse() {
            rank = PokerHand.FULL_HOUSE;
            name = "Full House";
        }

        public override bool IsValid( List<Card> cards ) {

            bool isFullHouse = false;
            for(int i = 0; i < cards.Count; i++) {
                //get a possible three of a kind from the cards
                List<Card> sameRankCards = GetSameRank ( cards[i].rank, cards );
                if(sameRankCards.Count == 3) {  //3 of a kind
                    //now get the 2 possible pairs, from the remaining cards (cards - sameRankCards = otherRankCards)
                    List<Card> otherRankCards = GetOtherRank ( cards[i].rank, cards );

                    List<List<Card>> pairCards = GetPairs (otherRankCards);

                    if(pairCards.Count >= 1) {
                        isFullHouse = true;
                        break;
                    }
                }
            }
            if(!isFullHouse)
                return false;

            return true;
        }

        /*Defines Full House Tie Breaker Rule. When it comes to full house
         * PlayerHand who has the highest 3 of a kind Pair will win. AAA22 beats KKKJJ
         * 
         */
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
                            playerHand.tieInfo = "3 Of A Kind High Card " + GetNames (sameRankCards);
                            
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
