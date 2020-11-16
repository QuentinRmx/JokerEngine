using System.Drawing.Imaging;

namespace JokerCore
{
    public class AbstractCardEffect : ICardEffect
    {
        // ATTRIBUTES

        public int EffectIdentifier { get; set; }

        public int InstanceId { get; set; }

         protected AbstractCardEffect? NextEffect { get; set; }


         public string Description { get; set; } = string.Empty;

        // CONSTRUCTORS

        public AbstractCardEffect(int effectIdentifier)
        {
            EffectIdentifier = effectIdentifier;
        }

        public AbstractCardEffect()
        {

        }

        // METHODS
        /// <inheritdoc />
        public virtual void Resolve(Card owner, CombatManager combatManager)
        {
        }

        public virtual AbstractCardEffect GetNext()
        {
            return null;
        }

        public virtual string GetDescription(Card card, CombatManager manager)
        {
            return string.Empty;
        }
    }
}