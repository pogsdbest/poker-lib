using Microsoft.VisualStudio.TestTools.UnitTesting;

using PokerLib;
using System.Collections.Generic;

namespace PokerLibUnitTest {
    [TestClass]
    public class PokerEvaluateTest {
        [TestMethod]
        public void Evaluate_PokerHandRankingTest() {
            //Test To Identify The Winner between 3 Players, Higher Poker hand Rank Wins
            //In This Test Nothing Beats Royal Flush, There Should be a Single Winner and it's ben

            //Arrange
            Poker poker = new Poker();
            List<PlayerHand> playerHands = new List<PlayerHand> ();
            PlayerHand benHand = new PlayerHand ( "ben", "QS,JS,10S,KS,AS" );   //Royal Flush
            PlayerHand joeHand = new PlayerHand ( "joe", "3S,4S,5S,2S,1S" );    //Straight Flush
            PlayerHand cloHand = new PlayerHand ( "clo", "AS,5D,5H,5C,5S" );    //Four Of A Kind

            playerHands.Add ( benHand );
            playerHands.Add ( joeHand );
            playerHands.Add ( cloHand );

            List<PlayerHand> expectedWinners = new List<PlayerHand> ();
            expectedWinners.Add ( benHand );                                    //Ben Should be The Only Winner
            PlayerHand expectedSingleWinner = benHand;

            //Act
            List<PlayerHand> winners = poker.Evaluate (playerHands);

            //Assert
            Assert.IsTrue ( (winners.Count == expectedWinners.Count), "Wrong Winner Count, There Should be A Single Winner" );
            Assert.AreEqual ( expectedSingleWinner, winners[0], "Wrong Expected Winner" );
        }

        [TestMethod]
        public void Evaluate_PokerHandRanking_MultipleWinnerTest() {
            //Test To Identify The Winner between 3 Players, Higher Poker hand Rank Wins
            //If Both Players Had Identical Cards or Tie, Both of Them Win and will split the pot

            //Arrange
            Poker poker = new Poker ();
            List<PlayerHand> playerHands = new List<PlayerHand> ();
            PlayerHand benHand = new PlayerHand ( "ben", "3D,4D,5D,2D,6D" );    //Straight Flush
            PlayerHand joeHand = new PlayerHand ( "joe", "3S,4S,5S,2S,6S" );    //Straight Flush
            PlayerHand bobHand = new PlayerHand ( "bob", "5H,5C,6H,6C,7S" );    //Two Pair

            playerHands.Add ( benHand );
            playerHands.Add ( joeHand );
            playerHands.Add ( bobHand );

            List<PlayerHand> expectedWinners = new List<PlayerHand> ();
            expectedWinners.Add ( benHand );                                    //Ben Won As Straight Flush With High Card 6D tie with joe, but beats bob
            expectedWinners.Add ( joeHand );                                    //Joe Won As Straight Flush With High Card 6S tie with ben, but beats bob
            

            //Act
            List<PlayerHand> winners = poker.Evaluate ( playerHands );

            //Assert
            Assert.IsTrue ( (winners.Count == expectedWinners.Count), "Wrong Winner Count, Expecting "+expectedWinners.Count+" winner(s), but Evaluate return "+winners.Count );
            foreach(PlayerHand expectedWinner in expectedWinners) {
                bool result = winners.Contains ( expectedWinner );
                Assert.IsTrue (result, "Wrong Winner. "+expectedWinner.playerName+" is not Expected to be the Winner.");
            }
        }

        [TestMethod]
        public void Evaluate_StraightFlush_TieBreakerTest() {
            //Test To Identify The Winner between 3 Players With The Same PokerHand Rank Straight Flush
            //If Both Players With Identical Cards The Highest Card Rank will decide. IF Both have the same High Card. they both win

            //Arrange
            Poker poker = new Poker ();
            List<PlayerHand> playerHands = new List<PlayerHand> ();
            PlayerHand benHand = new PlayerHand ( "ben", "3D,4D,5D,2D,6D" );    //Straight Flush High card 6D
            PlayerHand joeHand = new PlayerHand ( "joe", "AS,2S,3S,4S,5S" );    //Straight Flush Low card AS
            PlayerHand bobHand = new PlayerHand ( "bob", "3H,4H,5H,2H,6H" );    //Straight Flush High Card 6H

            playerHands.Add ( benHand );
            playerHands.Add ( joeHand );
            playerHands.Add ( bobHand );

            List<PlayerHand> expectedWinners = new List<PlayerHand> ();
            expectedWinners.Add ( benHand );                                    //Ben Won As Straight Flush With High Card 6D tie with bob, beats joe with only 5S
            expectedWinners.Add ( bobHand );                                    //bob Won As Straight Flush With High Card 6H tie with ben, beats joe with only 5S


            //Act
            List<PlayerHand> winners = poker.Evaluate ( playerHands );

            //Assert
            Assert.IsTrue ( (winners.Count == expectedWinners.Count), "Wrong Winner Count, Expecting " + expectedWinners.Count + " winner(s), but Evaluate return " + winners.Count );
            foreach(PlayerHand expectedWinner in expectedWinners) {
                bool result = winners.Contains ( expectedWinner );
                Assert.IsTrue ( result, "Wrong Winner. " + expectedWinner.playerName + " is not Expected to be the Winner." );
            }
        }

        [TestMethod]
        public void Evaluate_FourOFAKind_TieBreakerTest() {
            //Test To Identify The Winner between 3 Players With The Same PokerHand Rank Four OF A Kind
            //The Highest Card Combination Will Win

            //Arrange
            Poker poker = new Poker ();
            List<PlayerHand> playerHands = new List<PlayerHand> ();
            PlayerHand benHand = new PlayerHand ( "ben", "10S,5D,5H,5C,5S" );    //Four Of A Kind Rank 5
            PlayerHand joeHand = new PlayerHand ( "joe", "QS,KS,KD,KH,KC" );    //Straight Flush Rank King
            PlayerHand bobHand = new PlayerHand ( "bob", "6S,AD,AH,AC,AS" );    //Four Of A Kind Rank ACE HIGH

            playerHands.Add ( benHand );
            playerHands.Add ( joeHand );
            playerHands.Add ( bobHand );

            List<PlayerHand> expectedWinners = new List<PlayerHand> ();
            expectedWinners.Add ( bobHand );                                    //bob Won between joe and ben, ACE HIGH Beats any card Combination

            //Act
            List<PlayerHand> winners = poker.Evaluate ( playerHands );

            //Assert
            Assert.IsTrue ( (winners.Count == expectedWinners.Count), "Wrong Winner Count, Expecting " + expectedWinners.Count + " winner(s), but Evaluate return " + winners.Count );
            foreach(PlayerHand expectedWinner in expectedWinners) {
                bool result = winners.Contains ( expectedWinner );
                Assert.IsTrue ( result, "Wrong Winner. " + expectedWinner.playerName + " is not Expected to be the Winner." );
            }
        }

        [TestMethod]
        public void Evaluate_FullHouse_TieBreakerTest() {
            //Test To Identify The Winner between 3 Players With The Same PokerHand Full House
            //The Highest Card Combination Of 3 Of A Kind Will Win

            //Arrange
            Poker poker = new Poker ();
            List<PlayerHand> playerHands = new List<PlayerHand> ();
            PlayerHand benHand = new PlayerHand ( "ben", "3S,3D,3H,2C,2S" );    //Full House 3 OF A Kind Rank 3 Combination
            PlayerHand joeHand = new PlayerHand ( "joe", "QS,QH,QD,AH,AC" );    //Full House 3 OF A Kind Rank Queen Combination
            PlayerHand bobHand = new PlayerHand ( "bob", "KS,KD,KH,JC,JS" );    //Full House 3 OF A Kind Rank King Combination

            playerHands.Add ( benHand );
            playerHands.Add ( joeHand );
            playerHands.Add ( bobHand );

            List<PlayerHand> expectedWinners = new List<PlayerHand> ();
            expectedWinners.Add ( bobHand );                                    //bob Won between joe and ben, Higher Rank King Beats Queen and 3 Rank

            //Act
            List<PlayerHand> winners = poker.Evaluate ( playerHands );

            //Assert
            Assert.IsTrue ( (winners.Count == expectedWinners.Count), "Wrong Winner Count, Expecting " + expectedWinners.Count + " winner(s), but Evaluate return " + winners.Count );
            foreach(PlayerHand expectedWinner in expectedWinners) {
                bool result = winners.Contains ( expectedWinner );
                Assert.IsTrue ( result, "Wrong Winner. " + expectedWinner.playerName + " is not Expected to be the Winner." );
            }
        }

        [TestMethod]
        public void Evaluate_Flush_TieBreakerTest() {
            //Test To Identify The Winner between 3 Players With The Same PokerHand Rank Flush
            //The Player hand with the Highest Card Rank Win

            //Arrange
            Poker poker = new Poker ();
            List<PlayerHand> playerHands = new List<PlayerHand> ();
            PlayerHand benHand = new PlayerHand ( "ben", "3S,6S,8S,JS,AS" );    //Flush with Higher Card ACE HIGH
            PlayerHand joeHand = new PlayerHand ( "joe", "2S,4H,7D,6C,JC" );    //Flush with Higher Card Jack
            PlayerHand bobHand = new PlayerHand ( "bob", "3H,6H,8H,JH,KH" );    //Flush with Higher Card King

            playerHands.Add ( benHand );
            playerHands.Add ( joeHand );
            playerHands.Add ( bobHand );

            List<PlayerHand> expectedWinners = new List<PlayerHand> ();
            expectedWinners.Add ( benHand );                                    //ben Won between joe and bob, ACE HIGH beats jack and King Rank

            //Act
            List<PlayerHand> winners = poker.Evaluate ( playerHands );

            //Assert
            Assert.IsTrue ( (winners.Count == expectedWinners.Count), "Wrong Winner Count, Expecting " + expectedWinners.Count + " winner(s), but Evaluate return " + winners.Count );
            foreach(PlayerHand expectedWinner in expectedWinners) {
                bool result = winners.Contains ( expectedWinner );
                Assert.IsTrue ( result, "Wrong Winner. " + expectedWinner.playerName + " is not Expected to be the Winner." );
            }
        }

        [TestMethod]
        public void Evaluate_Straight_TieBreakerTest() {
            //Test To Identify The Winner between 3 Players With The Same PokerHand Straight
            //The Player hand with the Highest Card Rank Win

            //Arrange
            Poker poker = new Poker ();
            List<PlayerHand> playerHands = new List<PlayerHand> ();
            PlayerHand benHand = new PlayerHand ( "ben", "5S,4H,6H,8C,7S" );    //Straight with High Card 8C
            PlayerHand joeHand = new PlayerHand ( "joe", "AS,2H,3D,4C,5C" );    //Straight with High Card 5C
            PlayerHand bobHand = new PlayerHand ( "bob", "4D,3S,5D,7H,6S" );    //Straight with High Card 7H

            playerHands.Add ( benHand );
            playerHands.Add ( joeHand );
            playerHands.Add ( bobHand );

            List<PlayerHand> expectedWinners = new List<PlayerHand> ();
            expectedWinners.Add ( benHand );                                    //ben Won between joe and bob, 8C is The Highest Card between 5C and 7H

            //Act
            List<PlayerHand> winners = poker.Evaluate ( playerHands );

            //Assert
            Assert.IsTrue ( (winners.Count == expectedWinners.Count), "Wrong Winner Count, Expecting " + expectedWinners.Count + " winner(s), but Evaluate return " + winners.Count );
            foreach(PlayerHand expectedWinner in expectedWinners) {
                bool result = winners.Contains ( expectedWinner );
                Assert.IsTrue ( result, "Wrong Winner. " + expectedWinner.playerName + " is not Expected to be the Winner." );
            }
        }
    }
}
