using System.Runtime.Serialization;

namespace JokerCore
{
    public enum ECardCategory
    {
        [EnumMember(Value = "player")]
        Player,
        [EnumMember(Value = "enemy")]
        Enemy
    }
}