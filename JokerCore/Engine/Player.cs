using System.Collections.Generic;

namespace JokerCore
{
    public class Player
    {
        // ATTRIBUTES

        public string Name { get; set; }

        public Deck CurrentDeck { get; set; }
        
        public List<Card> Hand { get; set; } = new List<Card>();

        public int Health { get; private set; }
        
        public int MaxHeath { get; private set; }
        
        // CONSTRUCTORS

        public Player(string name)
        {
            Name = name;
        }

        // METHODS
    }
}