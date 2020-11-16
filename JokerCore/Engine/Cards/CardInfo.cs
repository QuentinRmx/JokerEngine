namespace JokerCore
{
    public struct CardInfo
    {
        public int InstanceId;
        public string Name;
        public int MaxHealth;
        public int Health;
        public int Attack;
        public int Identifier;
        public string Text;
        public int CardEffectAssociated;
        public int HealAmount;
        public int ArtworkIdentifier { get; set; }
        public int Cost { get; set; }
        public ECardType CardType { get; set; }

        public ECardCategory CardCategory;
    }
}