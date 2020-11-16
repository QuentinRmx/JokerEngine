namespace JokerCore
{
    public class NotAliveBehavior : AbstractAliveBehavior
    {
        // ATTRIBUTES

        // CONSTRUCTORS

        // METHODS


        /// <inheritdoc />
        public override bool TakeDamage(int damage)
        {
            return false;
        }

        /// <inheritdoc />
        public override bool Heal(int healAmount)
        {
            return false;
        }
    }
}