using MainSSM;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/ItemData")]
public class ItemData : ScriptableObject
{
    public GameObject itemPrefab;  // ������ ������
    [Range(0, 1)]
    public float dropChance;       // ������ ��� Ȯ�� (0 ~ 1)

    public ItemType itemType; // ������ ����
    public WaponType waponType;      // �ٰŸ� ���Ÿ� Ÿ��

    public bool isStackable;         // �������� ���� �� �ִ��� ����
    
    public int attackPower;          // ���ݷ�
    public int defensePower;         // ����
    public float attackSpeed;        // ���� �ӵ�
    public float Speed;              // �̵� �ӵ�
    public int healthRecovery;       // ü�� ȸ����


    public string itemCode; // ���ս� ����� ������ �ڵ�
    

    public Sprite icon;
}
public enum ItemType {
    HealthPotion,    // ü�� ȸ�� ������
    Weapon,          // ����
    Armor,           // ��
    Consumable,      // �Һ� ������
    Ingredient       // ��������
}