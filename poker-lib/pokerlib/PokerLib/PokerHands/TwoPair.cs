using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerLib.PokerHands {
    class TwoPair : PokerHand {
        public TwoPair() {
            rank = PokerHand.TWO_PAIR;
            name = "Two Pair";
        }

        public override bool IsValid( List<Card> cards ) {
            //Look for a possible 2 pairs of card set
            List<List<Card>> pairCards = GetPairs ( cards );
            if(pairCards.Count < 2) {
                return false;
            }

            return true;
        }

        public override List<PlayerHand> ProcessTieBreaker( List<PlayerHand> playerHands ) {
            List<PlayerHand> winners = new List<PlayerHand> ();

            List<Card> highest = new List<Card> ();
            List<Card> second = new List<Card> ();
            Card kicker = null;
            for(int i = 0; i < playerHands.Count; i++) {
                PlayerHand playerHand = playerHands[i];
                List<Card> cards = new List<Card>(playerHand.cardsInHand);

                List<List<Card>> pairCards = GetPairs ( cards );
                List<Card> highestPair = GetFirstHighestPair (pairCards);
                List<Card> secondPair = GetSecondPair (pairCards);
                cards.Remove ( highestPair[0] );
                cards.Remove ( highestPair[1] );
                cards.Remove ( secondPair[0] );
                cards.Remove ( secondPair[1] );
                //this is the 5th card
                Card remainingCard = cards[0];

                if(winners.Count == 0) {
                    playerHand.tieInfo += "1st Pair "+highestPair[0].name+","+highestPair[1].name;
                    playerHand.tieInfo += " 2nd Pair " + secondPair[0].name + "," + secondPair[1].name;
                    playerHand.tieInfo += " 5th Card " + remainingCard.name;
                    highest = highestPair;
                    second = secondPair;
                    kicker = remainingCard;
                    winners.Add ( playerHand );
                } else {
                    if(LeftPairHigher(highestPair,highest)) {
                        playerHand.tieInfo += "Highest Pair " + highestPair[0].name + "," + highestPair[1].name;
                        highest = highestPair;
                        second = secondPair;
                        kicker = remainingCard;
                        winners.Add ( playerHand );
                    } else if(EqualPair( highestPair, highest )) {
                        playerHand.tieInfo += "1st Pair " + highestPair[0].name + "," + highestPair[1].name;
                        if(LeftPairHigher(secondPair,second)) {
                            playerHand.tieInfo += " 2nd HighPair " + secondPair[0].name + "," + secondPair[1].name;
                            highest = highestPair;
                            second = secondPair;
                            kicker = remainingCard;
                            winners.Add ( playerHand );
                        } else if(EqualPair( secondPair, second )) {
                            playerHand.tieInfo += " 2nd Pair " + secondPair[0].name + "," + secondPair[1].name;
                            int remainingCardRank = remainingCard.rank;
                            int kickerRank = kicker.rank;
                            if(remainingCardRank == Card.ACE)
                                remainingCardRank = Card.ACE_HIGH;
                            if(kickerRank == Card.ACE)
                                kickerRank = Card.ACE_HIGH;
                            if(remainingCardRank > kickerRank) {
                                playerHand.tieInfo += " High Card " + remainingCard.name;
                                highest = highestPair;
                                second = secondPair;
                                kicker = remainingCard;
                                winners.Add ( playerHand );
                            } else if(remainingCardRank == kickerRank) {
                                playerHand.tieInfo += " 5th Card " + remainingCard.name;
                                winners.Add ( playerHand );
                            }
                        }
                    }
                }
            }

            return winners;
        }

        private List<Card> GetFirstHighestPair( List<List<Card>> pairCards) {
            List<Card> pair = new List<Card> ();
            int highestRank = 0;
            for(int i=0;i<pairCards.Count;i++) {
                List<Card> p = pairCards[i];
                int rank = p[0].rank;
                if(rank == Card.ACE) {
                    rank = Card.ACE_HIGH;
                }
                if(rank > highestRank) {
                    highestRank = rank;
                    pair = p;
                }
            }
            return pair;
        }

        private List<Card> GetSecondPair( List<List<Card>> pairCards ) {
            List<Card> pair = new List<Card> ();
            int lowestRank = Card.ACE_HIGH;
            for(int i = 0; i < pairCards.Count; i++) {
                List<Card> p = pairCards[i];
                int rank = p[0].rank;
                if(rank == Card.ACE) {
                    rank = Card.ACE_HIGH;
                }
                if(rank < lowestRank) {
                    lowestRank = rank;
                    pair = p;
                }
            }
            return pair;
        }

    }
}
