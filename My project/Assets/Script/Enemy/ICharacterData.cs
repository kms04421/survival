using UnityEngine;
namespace MainSSM
{
    public interface ICharacterData
    {
        int Hp { get; set; }
        float Speed { get; set; }
        int Damage { get; set; }

        int MaxHp { get; set; }
    }
}