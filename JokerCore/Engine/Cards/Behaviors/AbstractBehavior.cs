namespace JokerCore
{
    public abstract class AbstractBehavior : IBehavior
    {
        // ATTRIBUTES

        protected Card _card;

        // CONSTRUCTORS

        // METHODS


        /// <inheritdoc />
        public void Attach(Card card)
        {
            this._card = card;
        }

        /// <inheritdoc />
        public void Detach(Card card)
        {
            this._card = card;
        }
    }
}