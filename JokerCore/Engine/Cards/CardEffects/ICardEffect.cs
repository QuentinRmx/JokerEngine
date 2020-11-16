namespace JokerCore
{

    public interface ICardEffect
    {
        void Resolve(Card owner, CombatManager combatManager);
    }
}