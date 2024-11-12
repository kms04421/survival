using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/ItemData")]
public class ItemData : ScriptableObject
{
    public GameObject itemPrefab;  // 아이템 프리팹
    [Range(0, 1)]
    public float dropChance;       // 아이템 드롭 확률 (0 ~ 1)

    public ItemType itemType; // 아이템 종류

    public bool isStackable;         // 아이템이 쌓일 수 있는지 여부

    public int attackPower;          // 공격력
    public int defensePower;         // 방어력
    public int healthRecovery;       // 체력 회복량

}
public enum ItemType {
    HealthPotion,    // 체력 회복 아이템
    Weapon,          // 무기
    Armor,           // 방어구
    Consumable,      // 소비 아이템
}