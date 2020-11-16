using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JokerCore
{
    public class CardFactory
    {
        // ATTRIBUTES

        private static CardFactory _instance;

        private CollectionJson _collectionJsonJson;

        public static CardFactory Instance => _instance ??= new CardFactory();

        // CONSTRUCTORS

        private CardFactory()
        {
            LoadJsonData();
        }

        private void LoadJsonData()
        {
            string json = File.ReadAllText(Configuration.PathJsonCardData);
//            CollectionJson data = JsonSerializer.Deserialize<CollectionJson>(json);
            CollectionJson data = new CollectionJson();
            _collectionJsonJson = data;
        }

        // METHODS

        public Card Create(int cardId)
        {
            // TODO: Refactor factory to load card data from a file.
            //            CardEffectFactory.Instance.Serialize();

            CardInfo info;

            AbstractAliveBehavior behavior;
            info = _collectionJsonJson.CardInfos.FirstOrDefault(i => i.Identifier == cardId);
            AbstractCardEffect effect = CardEffectFactory.Instance.Create(info.CardEffectAssociated);
            // TODO: Serialize effects.
            switch (cardId)
            {
                case 0:
                    behavior = new AliveBehavior();
                    break;
                case 1:
                    behavior = new NotAliveBehavior();
                    break;
                case 2:
                    behavior = new NotAliveBehavior();
                    break;
                case 3:
                    behavior = new NotAliveBehavior();
                    break;
                case 4:
                    behavior = new NotAliveBehavior();
                    break;
                default:
                    behavior = new NotAliveBehavior();
                    return null;
            }

            Card card = new Card(effect) { AliveBehavior = behavior };
            info.InstanceId = InstanceIdManager.NextInstanceId;
            card.CardInfo = info;

            return card;
        }

        public IEnumerable<Card> GetAllCards()
        {
            List<Card> cards = new List<Card>();
            for (int i = 0; i <= _collectionJsonJson.LastIdentifier; i++)
            {
                cards.Add(Create(i));
            }

            return cards;
        }

        public void SerializeCards(IEnumerable<Card> cards)
        {
            IEnumerable<Card> enumerable = cards as Card[] ?? cards.ToArray();
            _collectionJsonJson.LastIdentifier = enumerable.Max(c => c.CardInfo.Identifier);
            _collectionJsonJson.CardInfos = enumerable.Select(c => c.CardInfo).ToList();
//            string json = JsonSerializer.Serialize(_collectionJsonJson);
            string json = string.Empty;
            File.WriteAllText(Configuration.PathJsonCardData, json);
        }
    }
}