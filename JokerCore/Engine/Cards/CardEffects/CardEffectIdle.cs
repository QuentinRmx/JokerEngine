namespace JokerCore
{
    public class CardEffectIdle : AbstractCardEffect
    {
        // ATTRIBUTES

        // CONSTRUCTORS

        /// <inheritdoc />
        public CardEffectIdle(int effectIdentifier) : base(effectIdentifier)
        {
        }

        public CardEffectIdle() : base()
        {

        }

        // METHODS


        /// <inheritdoc />
        public override void Resolve(Card owner, CombatManager combatManager)
        {
        }

        /// <inheritdoc />
        public override AbstractCardEffect GetNext()
        {
            return null;
        }

        /// <inheritdoc />
        public override string GetDescription(Card card, CombatManager manager)
        {
            return "Idle";
        }
    }
}