using MainSSM;

public class PlayerData : ICharacterData
{
    public int BaseHp { get; set; }
    public float BaseMovementSpeed { get; set; }
    public int BaseAttackPower { get; set; } // ���ݷ�
    public int MaxHp { get; set; } // �ִ�hp
    public int BaseDefensePower {  get; set; } // ����
    public float BaseAttackSpeed {  get; set; } // ���ݼӵ�

  
    public int AdditionalAttackPower = 0;   // �߰� ���ݷ�
 
    public int AdditionalDefensePower = 0;  // �߰� ����

    public float AdditionalAttackSpeed = 0; // �߰� ���� �ӵ�
    
    public float AdditionalMovementSpeed = 0; // �߰� �̵� �ӵ�


    public PlayerData(int _hp, float _speed, int _damage ,int _dfs,float _aps)      
    {
        BaseHp = _hp;
        BaseMovementSpeed = _speed;
        BaseAttackPower = _damage;
        BaseDefensePower = _dfs;
        BaseAttackSpeed = _aps;
        MaxHp = _hp;
    }
    public void AddMaxHP(int num)
    {
        MaxHp += num;
    }
    public void MinMaxHP(int num)
    {
        MaxHp -= num;
    }
    public void TakeDamage(int amount) //hp ������ ����
    {
        if (amount < BaseDefensePower) return;
        
        BaseHp -= amount - BaseDefensePower;
    }
    public void Heal(int amount) // ü�� ȸ��
    {
        if (BaseHp + amount > MaxHp)
        {
            BaseHp = MaxHp;
        }
        else
        {
            BaseHp += amount;
        }
    }
    // baseStateUP
    public void BaseAttackPowerUP(int amount) //��������
    {
        BaseAttackPower += amount;
    }
    public void BaseAttackPowerDown(int amount) //������ �ٿ�
    {
        BaseAttackPower -= amount;
    }
    public void BaseAttackSpeedUP(float amount) // ���Ӿ�
    {
        BaseAttackSpeed += amount;
    }
    public void BaseAttackSpeedDown(float amount) // ���� �ٿ�
    {
        BaseAttackSpeed -= amount;
    }
    public void BaseDefensePowerUp(int amount)//���¾�
    {
        BaseDefensePower += amount;
    }
    public void BaseDefensePowerDown(int amount)//���´ٿ�
    {
        BaseDefensePower -= amount;
    }
    public void BaseMovementSpeedUp(float amount)//���ǵ��
    {
        BaseMovementSpeed += amount;
    }
    public void BaseMovementSpeedDown(float amount)//���ǵ� �ٿ�
    {
        BaseMovementSpeed -= amount;
    }
    // baseStateUP


    // AddStateUp
    public void AdditionalStateUP(ItemData itemData)//�߰� �ɷ�ġ ��� ���
    {
        if (itemData == null) return;
        AdditionalAttackPower += itemData.attackPower ;
        AdditionalDefensePower += itemData.defensePower ; 
        AdditionalAttackSpeed += itemData.attackSpeed ;
        AdditionalMovementSpeed += itemData.Speed ;
    }
    public void AdditionalStateReset()//�߰� �ɷ�ġ �ʱ�ȭ
    {
        AdditionalAttackPower = 0;
        AdditionalDefensePower = 0;
        AdditionalAttackSpeed = 0;
        AdditionalMovementSpeed = 0;
    }

 
}
