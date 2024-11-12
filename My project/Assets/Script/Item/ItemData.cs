using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/ItemData")]
public class ItemData : ScriptableObject
{
    public GameObject itemPrefab;  // ������ ������
    [Range(0, 1)]
    public float dropChance;       // ������ ��� Ȯ�� (0 ~ 1)

    public ItemType itemType; // ������ ����

    public bool isStackable;         // �������� ���� �� �ִ��� ����

    public int attackPower;          // ���ݷ�
    public int defensePower;         // ����
    public int healthRecovery;       // ü�� ȸ����

}
public enum ItemType {
    HealthPotion,    // ü�� ȸ�� ������
    Weapon,          // ����
    Armor,           // ��
    Consumable,      // �Һ� ������
}