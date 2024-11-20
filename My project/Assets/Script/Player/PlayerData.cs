using MainSSM;

public class PlayerData : ICharacterData
{
    public int BaseHp { get; set; }
    public float BaseMovementSpeed { get; set; }
    public float BaseAttackPower { get; set; } // ���ݷ�
    public int MaxHp { get; set; } // �ִ�hp
    public float BaseDefensePower {  get; set; } // ����
    public float BaseAttackSpeed {  get; set; } // ���ݼӵ�

  
    public float AdditionalAttackPower = 0;   // �߰� ���ݷ�
 
    public float AdditionalDefensePower = 0;  // �߰� ����

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
    public void TakeDamage(float amount) //hp ������ ����
    {
        if (amount < (BaseDefensePower + AdditionalDefensePower)) return;
        
        BaseHp -= (int)amount - (int)(BaseDefensePower+ AdditionalDefensePower);

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
    public void ModifyBaseStat(int stateNumber, float amount)
    {
        switch (stateNumber)
        {
            case 1:
                BaseAttackPower += amount;
                break;
            case 2:
                BaseAttackSpeed += amount;
                break;
            case 3:
                BaseDefensePower += amount;             
                break;
            case 4:
                BaseMovementSpeed += amount;
                break;
            case 5:
                MaxHp += (int)amount;
                break;
        }
    }


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
