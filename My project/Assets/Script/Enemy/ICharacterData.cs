using UnityEngine;
namespace MainSSM
{
    public interface ICharacterData
    {
        int BaseHp { get; set; }
        float BaseMovementSpeed { get; set; }
        int BaseAttackPower { get; set; }

        int MaxHp { get; set; }
    }
}