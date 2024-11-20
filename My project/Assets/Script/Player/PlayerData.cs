using MainSSM;

public class PlayerData : ICharacterData
{
    public int BaseHp { get; set; }
    public float BaseMovementSpeed { get; set; }
    public float BaseAttackPower { get; set; } // 공격력
    public int MaxHp { get; set; } // 최대hp
    public float BaseDefensePower {  get; set; } // 방어력
    public float BaseAttackSpeed {  get; set; } // 공격속도

  
    public float AdditionalAttackPower = 0;   // 추가 공격력
 
    public float AdditionalDefensePower = 0;  // 추가 방어력

    public float AdditionalAttackSpeed = 0; // 추가 공격 속도
    
    public float AdditionalMovementSpeed = 0; // 추가 이동 속도


    public PlayerData(int _hp, float _speed, int _damage ,int _dfs,float _aps)      
    {
        BaseHp = _hp;
        BaseMovementSpeed = _speed;
        BaseAttackPower = _damage;
        BaseDefensePower = _dfs;
        BaseAttackSpeed = _aps;
        MaxHp = _hp;
    }
    public void TakeDamage(float amount) //hp 데미지 적용
    {
        if (amount < (BaseDefensePower + AdditionalDefensePower)) return;
        
        BaseHp -= (int)amount - (int)(BaseDefensePower+ AdditionalDefensePower);

    }
    public void Heal(int amount) // 체력 회복
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
    public void AdditionalStateUP(ItemData itemData)//추가 능력치 상승 상승
    {
        if (itemData == null) return;
        AdditionalAttackPower += itemData.attackPower ;
        AdditionalDefensePower += itemData.defensePower ; 
        AdditionalAttackSpeed += itemData.attackSpeed ;
        AdditionalMovementSpeed += itemData.Speed ;
    }
    public void AdditionalStateReset()//추가 능력치 초기화
    {
        AdditionalAttackPower = 0;
        AdditionalDefensePower = 0;
        AdditionalAttackSpeed = 0;
        AdditionalMovementSpeed = 0;
    }

 
}
