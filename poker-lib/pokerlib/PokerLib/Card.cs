using System;
using System.Collections.Generic;
using System.Text;

namespace PokerLib {
    public class Card {

		public const int CLUB = 1;
		public const int DIAMOND = 2;
		public const int HEART = 3;
		public const int SPADE = 4;

		public const int ACE = 1;
		public const int JACK = 11;
		public const int QUEEN = 12;
		public const int KING = 13;
		public const int ACE_HIGH = 14;

		public string name;
        public int rank = 0;
		public int suit = 0;

        public Card(string cardName) {
            name = cardName;
            AssignRank ();
			AssignSuit ();
        }

		public void AssignSuit () {
			char letter = '0';
			if(name[1] == '0') {
				letter = name[2];
			} else {
				letter = name[1];
			}
			if(letter == 'C') {
				suit = Card.CLUB;
			} else if(letter == 'D') {
				suit = Card.DIAMOND;
			} else if(letter == 'H') {
				suit = Card.HEART;
			} else if(letter == 'S') {
				suit = Card.SPADE;
			}
		}

        private void AssignRank() {
			if(name[0] == 'A' || (name[0] == '1' && name[1] != '0') ) {
				rank = ACE;
			} else if(name[0] == '2') {
				rank = 2;
			} else if(name[0] == '3') {
				rank = 3;
			} else if(name[0] == '4') {
				rank = 4;
			} else if(name[0] == '5') {
				rank = 5;
			} else if(name[0] == '6') {
				rank = 6;
			} else if(name[0] == '7') {
				rank = 7;
			} else if(name[0] == '8') {
				rank = 8;
			} else if(name[0] == '9') {
				rank = 9;
			} else if(name[0] == '1' && name[1] == '0') {
				rank = 10;
			} else if(name[0] == 'J') {
				rank = JACK;
			} else if(name[0] == 'Q') {
				rank = QUEEN;
			} else if(name[0] == 'K') {
				rank = KING;
			}
		}
    }
}
