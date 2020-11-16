namespace JokerCore
{
    public class AliveBehavior : AbstractAliveBehavior
    {
        // ATTRIBUTES

        // CONSTRUCTORS

        // METHODS


        /// <inheritdoc />
        public override bool TakeDamage(int damage)
        {
            _card.CardInfo.Health -= damage;
            _card.Update();
            return _card.CardInfo.Health <= 0;
        }

        /// <inheritdoc />
        public override bool Heal(int healAmount)
        {
            _card.CardInfo.Health += healAmount;
            if (_card.CardInfo.Health > _card.CardInfo.MaxHealth)
                _card.CardInfo.Health = _card.CardInfo.MaxHealth;

            return true;
        }
    }
}