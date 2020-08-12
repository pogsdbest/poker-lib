using System;
using System.Collections.Generic;
using System.Text;

namespace PokerLib.PokerHands {
    class Flush : PokerHand {

        public Flush() {
            rank = PokerHand.FLUSH;
            name = "Flush";
        }

        public override bool IsValid( List<Card> cards ) {
            //check if all same suit
            if(!IsSameSuit ( cards )) {
                return false;
            }

            return true;
        }

        /*Defines Flush Tie Breaker Rule.
         * Compares List Of Cards to another List Of Cards. Highest Card win
         * if both players have the same card ranks. they will win both
         */
        public override List<PlayerHand> ProcessTieBreaker( List<PlayerHand> playerHands ) {
            List<PlayerHand> winners = new List<PlayerHand> ();
            List<Card> currentHighest = new List<Card>();
            for(int i = 0; i < playerHands.Count; i++) {
                PlayerHand playerHand = playerHands[i];
                List<Card> cards = playerHand.cardsInHand;

                List<Card> handSortedCards = SortCards ( cards );

                if(winners.Count == 0) {
                    if(Contains ( handSortedCards, Card.ACE )) {
                        Card card = GetCard ( handSortedCards, Card.ACE );
                        playerHand.tieInfo = "ACE " + card.name;
                    } else {
                        playerHand.tieInfo = "High Card " + handSortedCards[handSortedCards.Count - 1].name;
                    }
                    currentHighest = handSortedCards;
                    winners.Add ( playerHand );
                } else {
                    //check for an Ace
                    if(Contains ( handSortedCards, Card.ACE ) && !Contains ( currentHighest, Card.ACE )) {
                        Card card = GetCard ( handSortedCards, Card.ACE );
                        playerHand.tieInfo = "ACE " + card.name;
                        winners.Clear ();
                        currentHighest = handSortedCards;
                        winners.Add ( playerHand );
                    } else if(Contains ( currentHighest, Card.ACE ) && !Contains ( handSortedCards, Card.ACE )) {
                        //if the current highest playerHand has an ACE and this playerHand has no, this one lose
                        continue;
                    } else { //check for an Equal or Compare if this player hand has the most highest Card Rank
                        bool isHigher = false;
                        bool isEqual = false;
                        for(int j = handSortedCards.Count - 1; j >= 0; j--) {
                            isEqual = false;
                            if(handSortedCards[j].rank > currentHighest[j].rank) {
                                playerHand.tieInfo += "High Card " + handSortedCards[j].name;
                                isHigher = true;
                                break;
                            } else if(handSortedCards[j].rank < currentHighest[j].rank) {
                                break;
                            } else if(handSortedCards[j].rank == currentHighest[j].rank) {
                                isEqual = true;
                            }
                        }

                        if(isHigher) {
                            winners.Clear ();
                            currentHighest = handSortedCards;
                            winners.Add ( playerHand );
                        } else if(isEqual) {
                            playerHand.tieInfo = "High Card " + handSortedCards[handSortedCards.Count - 1].name;
                            winners.Add ( playerHand );
                        }
                    }
                }
            }

            return winners;
        }

        
    }
}
