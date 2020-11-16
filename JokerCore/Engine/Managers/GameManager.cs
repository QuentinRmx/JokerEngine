using System.Collections.Generic;

namespace JokerCore
{
    public class GameManager
    {
        /// <summary>
        /// Represents the current main player.
        /// </summary>
        public Player? Player { get; set; }

        /// <summary>
        /// Manages the duels between the player and an opponent (either a bot or another player).
        /// </summary>
        public CombatManager CombatManager { get; }

        private readonly IBridge _bridge;

        // ATTRIBUTES


        // CONSTRUCTOR

        public GameManager(IBridge bridge)
        {
            _bridge = bridge;

            CombatManager = new CombatManager();
            CombatManager.OnCurrentManaChanged += _bridge.OnCurrentManaChange;
            CombatManager.OnCardAddedToHand += _bridge.AddCardToPlayerHand;
            CombatManager.OnDeckChanged += _bridge.OnDeckChange;
            CombatManager.OnCurrentHealthChanged += bridge.OnCurrentHealthChange;
            CombatManager.OnMaxHealthChanged += bridge.OnMaxHealthChange;
            CombatManager.OnPlayerDefeated += bridge.OnPlayerDefeat;
        }

        public void Init()
        {
            // TODO: Load enemies.
            AddEnemy(CardFactory.Instance.Create(0));
            Player = new Player("Evanzyker");

            // TODO: Load player hand.
            List<Card> deckTest = new List<Card>
            {
                new Card(new CardEffectNone())
                {
                    CardInfo = new CardInfo()
                    {
                        Attack = 1,
                        Health = 1,
                        MaxHealth = 1,
                        ArtworkIdentifier = 0,
                        Name = "Black Spirit",
                        Identifier = 0,
                        Cost = 1,
                        CardType = ECardType.Unit
                    }
                },
                new Card(new CardEffectNone())
                {
                    CardInfo = new CardInfo()
                    {
                        Attack = 2,
                        Health = 2,
                        MaxHealth = 2,
                        ArtworkIdentifier = 1,
                        Name = "Cursed Sailor",
                        Identifier = 1,
                        Cost = 2,
                        Text = "Deal 1 damage to another unit.",
                        CardType = ECardType.Unit
                    }
                },
                new Card(new CardEffectNone())
                {
                    CardInfo = new CardInfo()
                    {
                        Attack = 2,
                        Health = 2,
                        MaxHealth = 4,
                        ArtworkIdentifier = 2,
                        Name = "Cursed Captain",
                        Identifier = 2,
                        Cost = 3,
                        Text = "Grant your other units +1/+1.",
                        CardType = ECardType.Unit
                    }
                },
                new Card(new CardEffectNone())
                {
                    CardInfo = new CardInfo()
                    {
                        MaxHealth = 4,
                        ArtworkIdentifier = 3,
                        Name = "Shark Assault",
                        Identifier = 3,
                        Cost = 5,
                        Text = "Deal 3 damage to all units in a lane.",
                        CardType = ECardType.Unit
                    }
                }
            };
            Player.CurrentDeck = new Deck()
            {
                Cards = deckTest
            };

            CombatManager.SetPlayerDeck(deckTest);
            CombatManager.StartPlayerTurn();
        }

        // METHODS

        public void PlayCard(int instanceId)
        {
            CombatManager.PlayCardFromHand(instanceId);
        }

        public void RerollCard(int instanceId)
        {
            CombatManager.RerollCardInHand(instanceId);
            CombatManager.NbCardsRerolled++;
        }

        public void AddEnemy(Card card)
        {
            CombatManager?.AddEnemy(card);
            _bridge?.AddEnemy(card);
        }

        public void RemoveEnemy(Card card)
        {
            CombatManager.RemoveEnemy(card);
        }

        public void EndTurn()
        {
            CombatManager.EndPlayerTurn();
            // TODO: Add enemy turn logic here.
            CombatManager.StartPlayerTurn();
        }

        public int GetTurnNumber()
        {
            return CombatManager.CurrentTurn;
        }

        public int GetCurrentMana()
        {
            return CombatManager.CurrentMana;
        }

        public Player GetPlayer()
        {
            return CombatManager.Player1;
        }

        public Player GetOpponent()
        {
            return CombatManager.Player2;
        }

        public IEnumerable<Card> GetPlayerHand(ECardCategory category)
        {
            return CombatManager.GetPlayerHand();
        }
    }

    public enum ECardType
    {
        Unit,
        Spell
    }
}