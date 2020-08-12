using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PokerLib {
    public class PlayerHand {

        //name of the player
        public string playerName;

        //list of Cards that the player had
        public List<Card> cardsInHand;

        //After the poker hand is identified
        public string pokerHandName;
        public int rank;
        public String tieInfo;

        public PlayerHand(string playerName, string cardsString) {
            this.playerName = playerName;
            GenerateCardsInHand ( cardsString );
        }

        public PlayerHand(string playerName, List<Card> cards) {
            this.playerName = playerName;
            this.cardsInHand = cards;
        }

        private void GenerateCardsInHand(string cardsString ) {
            //removes white spaces, and separates card Characters by comma ','
            //ex: "3H ,3C   ,3S   ,3D,4 S" then the value would be = [0]="3H" [1]="3C" [2]="3S" [3]="3D [4]="4S"
            cardsString = Regex.Replace ( cardsString, @"\s+", "" );
            string[] cards = cardsString.Split (',');
            cardsInHand = new List<Card> ();
            for(int i = 0; i < cards.Length; i++) {
                Card card = new Card (cards[i]);
                cardsInHand.Add (card);
                
            }
        }

        public String GetCardString() {
            string cardString = cardsInHand[0].name;
            for(int i=1;i<cardsInHand.Count;i++) {

                cardString += ", "+cardsInHand[i].name;
            }
            return cardString;
        }
    }
}
