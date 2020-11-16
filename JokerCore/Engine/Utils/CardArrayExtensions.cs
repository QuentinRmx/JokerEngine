namespace JokerCore
{
    public static class CardArrayExtensions
    {

        // ATTRIBUTES

        // CONSTRUCTORS

        // METHODS

        /// <summary>
        /// Finds the given card in the Array and returns its position. If the card is not found, this extension method
        /// returns -1. This method uses the cards' instanceId to find the one passed in parameter with a direct
        /// equality.
        /// </summary>
        /// <param name="array">The array of card supposedly containing the card to find.</param>
        /// <param name="c">The card to find.</param>
        /// <returns>The position in the array of the card, or -1 if the card cannot be found.</returns>
        public static int IndexOf(this Card[] array, Card c)
        {
            for (int pos = 0; pos < array.Length; pos++)
            {
                if (array[pos].CardInfo.InstanceId == c.CardInfo.InstanceId)
                    return pos;
            }

            return -1;
        }


    }
}