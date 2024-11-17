using MainSSM;

public class PlayerData : ICharacterData
{
    public int BaseHp { get; set; }
    public float BaseMovementSpeed { get; set; }
    public int BaseAttackPower { get; set; } // 공격력
    public int MaxHp { get; set; } // 최대hp
    public int BaseDefensePower {  get; set; } // 방어력
    public float BaseAttackSpeed {  get; set; } // 공격속도

  
    public int AdditionalAttackPower = 0;   // 추가 공격력
 
    public int AdditionalDefensePower = 0;  // 추가 방어력

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
    public void AddMaxHP(int num)
    {
        MaxHp += num;
    }
    public void MinMaxHP(int num)
    {
        MaxHp -= num;
    }
    public void TakeDamage(int amount) //hp 데미지 적용
    {
        if (amount < BaseDefensePower) return;
        
        BaseHp -= amount - BaseDefensePower;
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
    public void BaseAttackPowerUP(int amount) //데미지업
    {
        BaseAttackPower += amount;
    }
    public void BaseAttackPowerDown(int amount) //데미지 다운
    {
        BaseAttackPower -= amount;
    }
    public void BaseAttackSpeedUP(float amount) // 공속업
    {
        BaseAttackSpeed += amount;
    }
    public void BaseAttackSpeedDown(float amount) // 공속 다운
    {
        BaseAttackSpeed -= amount;
    }
    public void BaseDefensePowerUp(int amount)//방어력업
    {
        BaseDefensePower += amount;
    }
    public void BaseDefensePowerDown(int amount)//방어력다운
    {
        BaseDefensePower -= amount;
    }
    public void BaseMovementSpeedUp(float amount)//스피드업
    {
        BaseMovementSpeed += amount;
    }
    public void BaseMovementSpeedDown(float amount)//스피드 다운
    {
        BaseMovementSpeed -= amount;
    }
    // baseStateUP


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
