using System;
using System.Collections.Generic;
using System.Text;
using PokerLib.PokerHands;

namespace PokerLib {
    public class Poker {

        private List<PokerHand> pokerHands;

        public Poker() {
            pokerHands = new List<PokerHand> ();
            pokerHands.Add ( new RoyalFlush() );
            pokerHands.Add ( new StraightFlush () );
            pokerHands.Add ( new FourOfAKind () );
            pokerHands.Add ( new FullHouse () );
            pokerHands.Add ( new Flush () );
            pokerHands.Add ( new Straight () );
            pokerHands.Add ( new ThreeOfAKind () );
            pokerHands.Add ( new TwoPair () );
            pokerHands.Add ( new OnePair () );
            pokerHands.Add ( new HighCard () );
        }

        public void Evaluate(List<PlayerHand> playerHands) {
            /*the Reason why i used a List On PlayerRankings is Because it  is possible that 2 or more players can win
             * if their card Ranks are identical, even they have different suit
             */
            
            List<PlayerHand> rankings = new List<PlayerHand> ();

            for(int i=0;i<playerHands.Count;i++) {
                PlayerHand playerHand = playerHands[i];

                for(int j=0;j<pokerHands.Count;j++) {
                    if(pokerHands[j].IsValid(playerHand.cardsInHand)) {
                        playerHand.pokerHandName = pokerHands[j].name;
                        playerHand.rank = pokerHands[j].rank;

                        break;
                    }
                }
                
                /*First Thing, Add The First PlayerHand
                *If there are another Player Hand, Compare The Rank of the player Hand with the existing
                *if the Existing PlayerHand rank is lower than the new PlayerHand, Remove the lower and place the Higher Rank
                *or if it the same Add it again for (Higher Card/Highest Pair Card Comparison) (Higher Card General Rule)
                **/
                if(rankings.Count == 0) {
                    rankings.Add ( playerHand );
                } else {
                    int highestPokerHandRank = rankings[0].rank;
                    if(playerHand.rank > highestPokerHandRank) {
                        rankings.Clear ();
                        rankings.Add (playerHand);
                    } else if(playerHand.rank == highestPokerHandRank) {
                        rankings.Add (playerHand);
                    }
                }
                Console.WriteLine (playerHand.playerName + " : "+playerHand.GetCardString()+" (" + playerHand.pokerHandName + ")");
            }

            //if the rankings Contains more than one, A tie breaks will be process As a General Rule High Rank Rule or Rank on Pair Rule
            //if Both Player Hands have identical cards, both player win
            if(rankings.Count > 1) {
                int pokerHandTieBreakerRank = rankings[0].rank;
                List<PlayerHand> winners = new List<PlayerHand> ();
                for(int i = 0; i < pokerHands.Count; i++) {
                    PokerHand pokerHand = pokerHands[i];
                    if(pokerHand.rank == pokerHandTieBreakerRank) {
                        winners = pokerHand.ProcessTieBreaker (rankings);
                        break;
                    }
                }

                if(winners.Count > 1) {
                    Console.WriteLine ( "Tie" );
                    for(int i=0;i<winners.Count;i++) {
                        PlayerHand playerHand = winners[i];
                        Console.WriteLine ( "Tie Breaker Winner " + playerHand.playerName + " PokerHand " + playerHand.pokerHandName + "(" + playerHand.tieInfo + ")" );
                    }
                } else {
                    PlayerHand playerHand = winners[0];
                    Console.WriteLine ( "Tie Breaker Winner " + playerHand.playerName + " PokerHand " + playerHand.pokerHandName + "(" + playerHand.tieInfo + ")" );
                }
            } else {
                PlayerHand playerHand = rankings[0];
                Console.WriteLine ("Winner "+playerHand.playerName + " PokerHand "+playerHand.pokerHandName );
            }

        }
    }
}
