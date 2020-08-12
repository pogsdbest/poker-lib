using System;
using System.Collections.Generic;
using System.Text;

namespace PokerLib.PokerHands {
    class HighCard : PokerHand {

        public HighCard() {
            rank = PokerHand.HIGH_CARD;
            name = "High Card";
        }

        public override bool IsValid( List<Card> cards ) {

            return true;
        }

        public override List<PlayerHand> ProcessTieBreaker( List<PlayerHand> playerHands ) {
            List<PlayerHand> winners = new List<PlayerHand> ();
            List<Card> highestCards = new List<Card> ();

            for(int i = 0; i < playerHands.Count; i++) {
                PlayerHand playerHand = playerHands[i];
                List<Card> cards = new List<Card> ( playerHand.cardsInHand );
                cards = ChangeAceToHighAce ( cards );
                cards = SortCards ( cards );


                if(winners.Count == 0) {
                    playerHand.tieInfo = "High Card " + cards[cards.Count - 1].name;
                    highestCards = cards;
                    
                    winners.Add ( playerHand );
                } else {
                    bool isEqual = false;
                    for(int j = cards.Count - 1; j >= 0; j--) {
                        Card card = cards[j];
                        Card highCard = highestCards[j];
                        int cardRank = card.rank;
                        int highCardRank = highCard.rank;
                        isEqual = false;
                        if(cardRank > highCardRank) {
                            playerHand.tieInfo = "High Card " + cards[j].name;
                            winners.Clear ();
                            highestCards = cards;
                            
                            winners.Add ( playerHand );
                            break;
                        } else if(cardRank == highCardRank) {
                            isEqual = true;
                            continue;
                        } else {
                            break;
                        }
                    }

                    if(isEqual) {
                        playerHand.tieInfo = "High Card " + cards[cards.Count - 1].name;
                        winners.Add ( playerHand );
                    }
                    
                }
            }

            return winners;
        }
    }
}
