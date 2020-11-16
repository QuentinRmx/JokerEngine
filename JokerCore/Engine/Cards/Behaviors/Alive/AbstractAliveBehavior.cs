namespace JokerCore
{
    public abstract class AbstractAliveBehavior : AbstractBehavior
    {
        /// <summary>
        /// Deals the damage passed in parameter to this card.
        /// If the card dies, its OnDeath event is raised.
        /// </summary>
        /// <param name="damage">The damage to take.</param>
        /// <returns>True if the cards died after taking the damage, false otherwise. Cards that cannot heal return
        /// false no matter what.</returns>
        public abstract bool TakeDamage(int damage);

        /// <summary>
        /// Heals the card for the given amount. The card cannot heal for more than its maximum health.
        /// </summary>
        /// <param name="healAmount">The amount of health this card get healed for.</param>
        /// <returns>True if the card </returns>
        public abstract bool Heal(int healAmount);
    }
}