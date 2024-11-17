using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/ItemData")]
public class ItemData : ScriptableObject
{
    public GameObject itemPrefab;  // ������ ������
    [Range(0, 1)]
    public float dropChance;       // ������ ��� Ȯ�� (0 ~ 1)

    public ItemType itemType; // ������ ����
    public WeaponType waponType;      // �ٰŸ� ���Ÿ� Ÿ��

    public bool isStackable;         // �������� ���� �� �ִ��� ����

    public int attackPower;          // ���ݷ�
    public int defensePower;         // ����
    public float attackSpeed;        // ���� �ӵ�
    public float Speed;              // �̵� �ӵ�
    public int healthRecovery;       // ü�� ȸ����


    public string itemCode; // ���ս� ����� ������ �ڵ�

    public Sprite icon;    //������ ������
}
public enum ItemType
{
    HealthPotion,    // ü�� ȸ�� ������
    Weapon,          // ����
    Headgear,        //�Ӹ� ��
    Armor,           //���� ��
    Accessory,       //�Ǽ�����
    Artifact,        //��Ƽ��Ʈ
    Consumable,      // �Һ� ������
    Ingredient       // ��������
}
public enum WeaponType
{
    Melee = 0,   // �ٰŸ� ����
    Ranged = 1,  // ���Ÿ� ����
    ExMelee = 2 //  Ư�� �ٰŸ�
}