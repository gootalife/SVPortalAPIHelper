using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace SVPortalAPIHelper.Entity {
    public class Deck {
        public Deck(int clan, List<Card> cards) : this() {
            Clan = clan;
            Cards = cards;
        }

        public Deck() {
            Clan = 0;
            Cards = new List<Card>();
        }

        public int Clan { get; set; }

        public List<Card> Cards { get; set; }
    }
}
