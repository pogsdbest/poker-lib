using System;
using System.Collections.Generic;
using System.Text;

namespace PokerLib.PokerHands {
    class OnePair : PokerHand {
        public OnePair() {
            rank = PokerHand.ONE_PAIR;
            name = "One Pair";
        }

        public override bool IsValid( List<Card> cards ) {
            //Look for a possible 2 pairs of card set
            List<List<Card>> pairCards = GetPairs ( cards );
            if(pairCards.Count < 1) {
                return false;
            }

            return true;
        }
        public override List<PlayerHand> ProcessTieBreaker( List<PlayerHand> playerHands ) {
            List<PlayerHand> winners = new List<PlayerHand> ();
            List<Card> highestPair = new List<Card> ();
            List<Card> highestCards = new List<Card> ();
            for(int i = 0; i < playerHands.Count; i++) {
                PlayerHand playerHand = playerHands[i];
                List<Card> cards = new List<Card> ( playerHand.cardsInHand );

                List<List<Card>> pairCards = GetPairs ( cards );
                List<Card> pair = pairCards[0];
                List<Card> otherCards = GetOtherRank ( pair[0].rank, cards );

                if(winners.Count == 0) {
                    playerHand.tieInfo = "One Pair " + pair[0].name + "," + pair[1].name + " " + GetNames (otherCards);
                    
                    highestPair = pair;
                    highestCards = otherCards;
                    winners.Add ( playerHand );
                } else {
                    if(LeftPairHigher(pair,highestPair)) {
                        playerHand.tieInfo = "High Pair " + pair[0].name + "," + pair[1].name;
                        winners.Clear ();
                        highestPair = pair;
                        highestCards = otherCards;
                        winners.Add ( playerHand );
                    } else if(EqualPair ( pair, highestPair )) {
                        playerHand.tieInfo = "One Pair " + pair[0].name + "," + pair[1].name+ " ";
                        highestCards = ChangeAceToHighAce ( highestCards);
                        otherCards = ChangeAceToHighAce ( otherCards);
                        highestCards = SortCards (highestCards);
                        otherCards = SortCards (otherCards);
                        bool isEqual = false;
                        for(int j=otherCards.Count -1;j >= 0;j--) {
                            Card otherCard = otherCards[j];
                            Card highCard = highestCards[j];
                            int otherCardRank = otherCard.rank;
                            int highCardRank = highCard.rank;
                            isEqual = false;
                            if(otherCardRank > highCardRank) {
                                playerHand.tieInfo += "High Card " + otherCard.name;
                                winners.Clear ();
                                highestPair = pair;
                                highestCards = otherCards;
                                winners.Add ( playerHand );
                                break;
                            } else if(otherCardRank == highCardRank) {
                                playerHand.tieInfo += otherCard.name + ",";
                                isEqual = true;
                                continue;
                            } else {
                                break;
                            }
                        }

                        if(isEqual) {
                            winners.Add ( playerHand );
                        }
                    }
                }
            }

            return winners;
        }
    }
}
