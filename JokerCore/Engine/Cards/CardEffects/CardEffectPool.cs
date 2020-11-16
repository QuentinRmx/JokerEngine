using System;
using System.Linq;

namespace JokerCore
{
    public class CardEffectPool : AbstractCardEffect
    {
        // ATTRIBUTES

        public AbstractCardEffect[] Pool { get; set; }

        // CONSTRUCTORS

        public CardEffectPool(int effectIdentifier, AbstractCardEffect[] pool) : base(effectIdentifier)
        {
            Pool = pool;
        }

        // METHODS


        /// <inheritdoc />
        public override void Resolve(Card owner, CombatManager combatManager)
        {
            // Pick a random effect amongst the one available.
            ICardEffect effect = GetNext();
            effect?.Resolve(owner, combatManager);
            NextEffect = null;
        }

        /// <inheritdoc />
        public override AbstractCardEffect GetNext()
        {
            Random rng = new Random();
            return NextEffect ??= Pool.ToList().OrderBy(x => rng.Next(100)).Take(1).FirstOrDefault();
        }

        /// <inheritdoc />
        public override string GetDescription(Card card, CombatManager manager)
        {
            return GetNext().GetDescription(card, manager);
        }
    }
}