using System.Collections.Generic;
using System.Linq;

namespace JokerCore
{
    // TODO: Manage enemy placement on a 3x3 grid.
    public class BoardManager
    {
        private const int MAX_ENEMIES = 9;

        private const int BOARD_WIDTH = 5;

        private readonly List<Card> _enemies;

        private readonly Card[][] _board;

        public BoardManager()
        {
            _enemies = new List<Card>();
            _board = new Card[BOARD_WIDTH][];
        }

        /// <summary>
        /// Adds the enemy card to the enemy board and returns true. If the enemy board is already full, than the new
        /// enemy card is not added and this method returns false.
        /// </summary>
        /// <param name="c">The enemy card to add. It's the GameManager's responsibility to make sure that it's a valid
        /// enemy card.</param>
        /// <returns>True if the enemy has been correctly added to the board, false otherwise.</returns>
        public bool AddEnemy(Card c)
        {
            if (_enemies.Count >= MAX_ENEMIES)
            {
                return false;
            }
            _enemies.Add(c);
            return true;
        }

        public bool AddEnemy(Card c, int x, int y)
        {
            if (_board[x][y] != null)
            {
                return false;
            }
            _board[x][y] = c;
            return true;
        }

        public void RemoveEnemy(Card c)
        {
            _enemies.Remove(c);
        }

        public void ClearEnemies()
        {
            _enemies.Clear();
        }

        public List<Card> GetEnemyBoard()
        {
            return _enemies;
        }

        public Card GetEnemyByInstanceId(int instanceId)
        {
            return _enemies.FirstOrDefault(e => e.CardInfo.InstanceId == instanceId);
        }

        public int GetEnemiesCount()
        {
            return _enemies.Count;
        }
    }
}