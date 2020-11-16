using System;
using System.Collections.Generic;
using System.Linq;

namespace JokerCore
{
    /// <summary>
    /// This class manages everything related to the combat part of the engine.<br/>
    /// It's currently designed with the following features/principles/ideas in mind:<br/>
    /// - The player can have 10 cards in hand at max.<br/>
    /// - The cards are always drawn one by one from any source (deck, graveyard, banish, etc).<br/>
    /// </summary>
    public class CombatManager
    {
        // CONSTANTS

        /// <summary>
        /// Default hand size used to initialize the CombatManager with valid values. 
        /// </summary>
        public const int DEFAULT_PLAYER_HAND_SIZE = 10;

        /// <summary>
        /// Default board size for both players. Each board is a [BOARD_SIZE][BOARD_SIZE] matrix.
        /// </summary>
        public const int BOARD_SIZE = 5;

        #region EVENTS

        /// <summary>
        /// Raised when a Card is added to the player's hand. The argument is the position of the card.
        /// </summary>
        public event EventHandler<int> OnCardAddedToHand;

        /// <summary>
        /// Raised when any modification (add, remove cards) is done.
        /// </summary>
        public event EventHandler<Card> OnDeckChanged;

        /// <summary>
        /// Raised when an enemy enters the board.
        /// This event should ONLY be raised by the <see cref="AddEnemy"/> method.
        /// </summary>
        public event EventHandler<Card> OnEnemyUnitDeployed;

        /// <summary>
        /// Raised when the current amount of mana is updated.
        /// </summary>
        public event EventHandler<int> OnCurrentManaChanged;

        public event EventHandler<int> OnCurrentHealthChanged;

        public event EventHandler<int> OnMaxHealthChanged;

        public event EventHandler OnPlayerDefeated;

        #endregion

        // ATTRIBUTES

        private readonly BoardManager _boardManager;

        // TODO: Refactor hand size into a constant or config file eventually.
        private readonly Card[] _playerHand = new Card[DEFAULT_PLAYER_HAND_SIZE];

        private readonly List<Card> _playerDeck = new List<Card>();

        private readonly List<Card> _graveyard = new List<Card>();

        // TODO: Refactor mana in a player class.

        private int _currentMana;

        public int CurrentMana
        {
            get => _currentMana;
            set
            {
                _currentMana = value;
                OnCurrentManaChanged?.Invoke(this, _currentMana);
            }
        }

        public int MaxMana { get; private set; }

        private int _currentHealth;

        public int CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                OnCurrentHealthChanged?.Invoke(this, _currentHealth);
                if (_currentHealth <= 0)
                {
                    OnPlayerDefeated?.Invoke(this, null);
                }
            }
        }

        private int _maxHealth;

        public int MaxHealth
        {
            get => _maxHealth;
            set
            {
                _maxHealth = value;
                OnMaxHealthChanged?.Invoke(this, _maxHealth);
            }
        }


        /// <summary>
        /// Player 1 is usually the main player.
        /// </summary>
        public Player Player1 { get; set; }

        /// <summary>
        /// Player 2 is either a bot or another player.
        /// </summary>
        public Player Player2 { get; set; }

        /// <summary>
        /// Current turn. Starts from 1 and can go up indefinitely in theory.
        /// </summary>
        public int CurrentTurn { get; set; }


        // STATS

        public int NbCardsPlayed { get; set; } = 0;

        public int NbCardsRerolled { get; set; } = 0;

        // CONSTRUCTOR

        public CombatManager()
        {
            // TODO: Magic number.
            MaxMana = 3;
            CurrentMana = 3;
            CurrentHealth = 100;
            MaxHealth = 100;
            CurrentTurn = 1;
        }

        // METHODS

        public Card GetEnemyByInstanceId(int id)
        {
            return _boardManager.GetEnemyByInstanceId(id);
        }

        public int GetPlayerDeckSize()
        {
            return _playerDeck.Count;
        }

        public IEnumerable<Card> GetGraveyard()
        {
            return _graveyard;
        }

        public void SetPlayerDeck(IEnumerable<Card> deck)
        {
            foreach (Card card in deck)
            {
                AddCardToDeck(card);
            }
        }

        /// <summary>
        /// Bind the OnDeath event of the enemy card to the RemoveEnemy method and then pass the card to the
        /// BoardManager.
        /// If the enemy card has been added to the board then the <see cref="OnEnemyAdded"/> event is raised.
        /// This should be the only place this event is called no matter what.
        /// </summary>
        /// <param name="enemyCard">The enemy card to add to the board.</param>
        public void AddEnemy(Card enemyCard)
        {
            enemyCard.OnDeath += (sender, enemy) => RemoveEnemy(enemy);
            bool correctlyAdded = _boardManager.AddEnemy(enemyCard);
            if (correctlyAdded)
            {
                OnEnemyUnitDeployed?.Invoke(this, enemyCard);
            }
        }

        public void RemoveEnemy(Card card)
        {
            _boardManager.RemoveEnemy(card);
        }

        public List<Card> GetEnemyBoard()
        {
            return _boardManager.GetEnemyBoard();
        }

        public Card[] GetPlayerHand()
        {
            return _playerHand;
        }


        //        public void AddCardToHand(Card c)
        //        {
        //            _playerHand.Add(c);
        //            OnCardAddedToHand?.Invoke(c, _playerHand.IndexOf(c));
        //        }

        public void AddCardToHand(Card c, int position)
        {
            _playerHand[position] = c;
            OnCardAddedToHand?.Invoke(c, position);
        }

        public void PlayCardFromHand(int instanceId)
        {
            Card? card = GetPlayerHand().FirstOrDefault(c => c.CardInfo.InstanceId == instanceId);
            if (card != null)
            {
                int pos = _playerHand.IndexOf(card);
                card.Play(this);
                _playerHand[pos] = null;
                _graveyard.Add(card);
                NbCardsPlayed++;
                CurrentMana--;
                DrawRandomCard(pos);
            }
        }

        /// <summary>
        /// End the player's turn by following this sequence of action :<br/>
        /// 1. The hand is added to the graveyard.<br/>
        /// 2. The hand is cleared.<br/><br/>
        /// Once all those actions have been done with success, the <see cref="StartEnemyTurn"/> method is called.
        /// </summary>
        public void EndPlayerTurn()
        {
            // TODO: Implement additional end of turn effects (cleaning hand?/checking active status effects).
            _graveyard.AddRange(_playerHand);
            ClearPlayerHand();
            StartEnemyTurn();
        }

        public void StartEnemyTurn()
        {
            List<Card> enemies = GetEnemyBoard();
            foreach (Card enemy in enemies)
            {
                enemy.Play(this);
            }
        }

        public void EndEnemyTurn()
        {
        }

        public void StartPlayerTurn()
        {
            CurrentMana = MaxMana;
            CurrentTurn++;
            InitHandWithRandomCards(DEFAULT_PLAYER_HAND_SIZE);
        }

        /// <summary>
        /// Draw a given amount of cards randomly amongst the ones currently in deck.
        /// If the deck gets emptied during the process, the graveyard is shuffled back into it.
        /// If there is not enough card in the deck and graveyard combined the remaining cards to draw are ignored
        /// and the method returns.<br/><br/>
        /// It must be noted that this method will replace the hand starting from the index 0.<br/>
        /// By definition, this method should be called when drawing a full hand from scratch and not to replace
        /// multiple cards at once. Instead, the <see cref="DrawRandomCard"/> should be used.
        /// </summary>
        /// <param name="amount">The amount of card to draw from the deck.</param>
        public void InitHandWithRandomCards(int amount)
        {
            int realAmount = Math.Max(DEFAULT_PLAYER_HAND_SIZE, amount);
            for (int pos = 0; pos < realAmount; pos++)
            {
                DrawRandomCard(pos);
            }
        }

        /// <summary>
        /// Draw a random card and place it at the given position.
        /// </summary>
        /// <param name="position"></param>
        public void DrawRandomCard(int position)
        {
            if (_playerDeck.Count == 0)
            {
                // TODO: Design empty-deck behavior and implement it there.
                ShuffleGraveyardIntoDeck();
            }

            Card c = _playerDeck.OrderBy(x => new Random().Next(100)).Take(1).First();
            _playerDeck.Remove(c);
            AddCardToHand(c, position);
            OnDeckChanged?.Invoke(this, c);
        }

        public void RerollCardInHand(int instanceId)
        {
            Card card = _playerHand.First(c => c.CardInfo.InstanceId == instanceId);
            card.Reroll(this);
            int position = _playerHand.IndexOf(card);
            _graveyard.Add(card);
            _playerHand[position] = null;
            DrawRandomCard(position);
            CurrentMana--;
        }

        public void ShuffleGraveyardIntoDeck()
        {
            foreach (Card c in _graveyard)
            {
                AddCardToDeck(c);
            }

            _graveyard.Clear();
        }

        private void AddCardToDeck(Card card)
        {
            _playerDeck.Add(card);
            OnDeckChanged?.Invoke(this, card);
        }

        /// <summary>
        /// This method clears all references in the <see cref="_playerHand"/> array.<br/>
        /// This method must be used only after copying the instance somewhere else otherwise the card will be
        /// permanently deleted.
        /// </summary>
        private void ClearPlayerHand()
        {
            for (int pos = 0; pos < _playerHand.Length; pos++)
            {
                _playerHand[pos] = null;
            }
        }
    }
}