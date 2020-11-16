using System;
using System.Collections.Generic;
using System.Linq;

namespace JokerCore
{
    public class CardEffectCriticalStrike : CardEffectAttack
    {

        // ATTRIBUTES

        public float DamageMultiplier { get; set; } = 2f;

        // CONSTRUCTORS

        // METHODS


        /// <inheritdoc />
        public CardEffectCriticalStrike(int effectIdentifier, ETargetSelector selector) : base(effectIdentifier, selector)
        {
        }

        /// <inheritdoc />
        public override void Resolve(Card owner, CombatManager combatManager)
        {
            Random rng = new Random();
            int damage = CalculateDamage(owner, combatManager);
            switch (_selector)
            {
                case ETargetSelector.AllEnemy:
                    Card c;
                    List<int> targetIds = combatManager.GetEnemyBoard().Select(ca => ca.CardInfo.InstanceId).ToList();
                    targetIds.ForEach(id =>
                    {
                        c = combatManager.GetEnemyByInstanceId(id);
                        c.TakeDamage(damage);
                    });
                    break;
                case ETargetSelector.RandomEnemy:
                    List<Card> targets = combatManager.GetEnemyBoard();
                    int randomTarget = rng.Next(targets.Count);
                    targets[randomTarget].TakeDamage(damage);
                    break;
                case ETargetSelector.Player:
                    combatManager.CurrentHealth -= damage;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <inheritdoc />
        public override string GetDescription(Card card, CombatManager manager)
        {
            return $"Critical strike for {CalculateDamage(card, manager)}";
        }

        /// <summary>
        /// Calculates the damage this effect deals depending on the current situation.
        /// Any damage modifiers is retrieved from the combatManager.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="combatManager"></param>
        /// <returns></returns>
        protected new int CalculateDamage(Card owner, CombatManager combatManager)
        {
            // Logic here to change damage calculation
            return (int)(owner.CardInfo.Attack * DamageMultiplier);
        }
    }
}