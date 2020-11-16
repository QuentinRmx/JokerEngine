namespace JokerCore
{
    public interface IBehavior
    {

        /// <summary>
        /// Attaches the behavior to the card. Any binding and initialization should go here.
        /// </summary>
        /// <param name="card">The card to bind with.</param>
        void Attach(Card card);


        /// <summary>
        /// Detaches the behavior from the card. Any binding must be removed here.
        /// </summary>
        /// <param name="card">The card to detach from.</param>
        void Detach(Card card);
    }
}