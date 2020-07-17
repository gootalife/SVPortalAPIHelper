namespace SVPortalAPIHelper.Entity {
    public class Card {
        public int CardId { get; }
        public string CardName { get; }
        public int CharType { get; }
        public int Clan { get; }
        public int Cost { get; }
        public int Atk { get; }
        public int Life { get; }
        public int EvoAtk { get; }
        public int EvoLife { get; }
        public string TribeName { get; }
        public string Skill { get; }
        public int Rarity { get; }
        public string CardImageURL { get; }
        public string CostImageURL { get; }
        public string RarityImageURL { get; }

        public Card(int cardId, string cardName, int charType, int clan, int cost, int atk, int life,
            int evoAtk, int evoLife, string tribeName, string skill, int rarity) {
            CardId = cardId;
            CardName = cardName;
            CharType = charType;
            Clan = clan;
            Cost = cost;
            Atk = atk;
            Life = life;
            EvoAtk = evoAtk;
            EvoLife = evoLife;
            TribeName = tribeName;
            Skill = skill;
            Rarity = rarity;
            CardImageURL = $"https://shadowverse-portal.com/image/card/phase2/common/L/L_{CardId}.jpg";
            CostImageURL = $"https://shadowverse-portal.com/public/assets/image/common/global/cost_{Cost}.png";
            string[] strRarity = {"bronze", "silver", "gold", "legend"};
            RarityImageURL = $"https://shadowverse-portal.com/public/assets/image/common/ja/rarity_{strRarity[Rarity-1]}.png";
        }
    }
}
