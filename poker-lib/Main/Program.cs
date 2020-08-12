using System;
using System.Collections.Generic;
using PokerLib;

namespace Main {
    class Program {
        static void Main( string[] args ) {

            Poker poker = new Poker ();

            Console.WriteLine ( "Poker Hands Ranking");
            List<PlayerHand> pokerHandsRanking = new List<PlayerHand> ();
            pokerHandsRanking.Add ( new PlayerHand ( "ben", "QS,JS,10S,KS,AS" ) ); //Royal Flush
            pokerHandsRanking.Add ( new PlayerHand ( "gg", "3S,4S,5S,2S,1S" ) ); //Straight Flush
            pokerHandsRanking.Add ( new PlayerHand ( "cloe", "AS,5D,5H,5C,5S" ) ); //Four Of A Kind
            pokerHandsRanking.Add ( new PlayerHand ( "amp", "10C,10H,3C,3D,3S" ) ); //Full House
            pokerHandsRanking.Add ( new PlayerHand ( "joe", "3H,6H,8H,JH,KH" ) ); //Flush
            pokerHandsRanking.Add ( new PlayerHand ( "vivi", "5S,4D,6H,8C,7S" ) ); //Straight
            pokerHandsRanking.Add ( new PlayerHand ( "jen", "3C,3D,3S,8C,10H") ); //Three Of A Kind
            pokerHandsRanking.Add ( new PlayerHand ( "alt", "5S,5D,6H,6C,7S" ) ); //Two Pair
            pokerHandsRanking.Add ( new PlayerHand ( "qq", "6S,5D,3H,5C,7S" ) ); //One Pair
            pokerHandsRanking.Add ( new PlayerHand ( "bob", "2H,5C,7S,10C,AC") ); //High Card       
            poker.Evaluate ( pokerHandsRanking );

            Console.WriteLine ("Straight Flush Tie Breaker");
            List<PlayerHand> straightFlushTieBreaker = new List<PlayerHand> ();
            straightFlushTieBreaker.Add ( new PlayerHand ( "ben", "3D,4D,5D,2D,6D" ) ); //Straight Flush
            straightFlushTieBreaker.Add ( new PlayerHand ( "bob", "3S,4S,5S,2S,6S" ) ); //Straight Flush 
            straightFlushTieBreaker.Add ( new PlayerHand ( "joe", "AS,2S,3S,4S,5S" ) ); //Straight Flush 
            poker.Evaluate ( straightFlushTieBreaker );

            Console.WriteLine ( "Royal Flush Tie Breaker" );
            List<PlayerHand> royalFlushTie = new List<PlayerHand> ();
            royalFlushTie.Add ( new PlayerHand ( "ben", "QS,JS,10S,KS,AS" ) ); //Royal Flush
            royalFlushTie.Add ( new PlayerHand ( "bob", "QD,JD,10D,KD,AD" ) ); //Royal Flush
            poker.Evaluate ( royalFlushTie );

            Console.WriteLine ( "4 Of A Kind Tie Breaker" );
            List<PlayerHand> fourOfAkindTie = new List<PlayerHand> ();
            fourOfAkindTie.Add ( new PlayerHand ( "ben", "AS,5D,5H,5C,5S" ) ); //4 Of A Kind
            fourOfAkindTie.Add ( new PlayerHand ( "bob", "5S,AD,AH,AC,AS" ) ); //4 Of A Kind
            poker.Evaluate ( fourOfAkindTie );

            Console.WriteLine ( "Full House Tie Breaker" );
            List<PlayerHand> fullHouseTie = new List<PlayerHand> ();
            fullHouseTie.Add ( new PlayerHand ( "ben", "3S,3D,3H,2C,2S" ) ); //3 Of A Kind
            fullHouseTie.Add ( new PlayerHand ( "bob", "KS,KD,KH,JC,JS" ) ); //3 Of A Kind
            poker.Evaluate ( fullHouseTie );

            Console.WriteLine ( "Flush Tie Breaker" );
            List<PlayerHand> flushTie = new List<PlayerHand> ();
            flushTie.Add ( new PlayerHand ( "ben", "3H,6H,8H,JH,KH" ) ); //Flush
            flushTie.Add ( new PlayerHand ( "bob", "3S,6S,8S,JS,AS" ) ); //Flush
            poker.Evaluate ( flushTie );

            Console.WriteLine ( "Straight Tie Breaker" );
            List<PlayerHand> straightTie = new List<PlayerHand> ();
            straightTie.Add ( new PlayerHand ( "ben", "5S,4D,6H,8C,7S" ) ); //Straight
            straightTie.Add ( new PlayerHand ( "bob", "4D,3S,5D,7H,6S" ) ); //Straight
            poker.Evaluate ( straightTie );

            Console.WriteLine ( "3 Of A Kind Tie Breaker" );
            List<PlayerHand> threeOfAKindTie = new List<PlayerHand> ();
            threeOfAKindTie.Add ( new PlayerHand ( "ben", "AS,AD,AH,2C,3S" ) ); //3 Of A Kind
            threeOfAKindTie.Add ( new PlayerHand ( "bob", "KS,KD,KH,JC,QS" ) ); //3 Of A Kind
            poker.Evaluate ( threeOfAKindTie );

            Console.WriteLine ( "Two Pair Tie Breaker" );
            List<PlayerHand> twoPairTie = new List<PlayerHand> ();
            twoPairTie.Add ( new PlayerHand ( "ben", "AH,AC,6H,6C,7S" ) ); //Two Pair
            twoPairTie.Add ( new PlayerHand ( "bob", "5S,5D,6S,6D,7D" ) ); //Two Pair
            poker.Evaluate ( twoPairTie );

            Console.WriteLine ( "One Pair Tie Breaker" );
            List<PlayerHand> onePairTie = new List<PlayerHand> ();
            onePairTie.Add ( new PlayerHand ( "ben", "6S,5S,3H,5H,AS" ) ); //One Pair
            onePairTie.Add ( new PlayerHand ( "bob", "6S,5D,3H,5C,7S" ) ); //One Pair
            poker.Evaluate ( onePairTie );

            Console.WriteLine ( "High Card Tie Breaker" );
            List<PlayerHand> highCardTie = new List<PlayerHand> ();
            highCardTie.Add ( new PlayerHand ( "ben", "2H,5C,7S,10C,3C" ) ); //High Card
            highCardTie.Add ( new PlayerHand ( "bob", "2S,5D,7H,10S,KD" ) ); //High Card
            poker.Evaluate ( highCardTie );
        }
    }
}
