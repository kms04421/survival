using MainSSM;

public class PlayerData : ICharacterData
{
    public int Hp { get; set; }
    public float Speed { get; set; }
    public int Damage { get; set; }
    public int MaxHp { get; set; }

    public int DFS {  get; set; }

    public float APS {  get; set; }
    public PlayerData(int _hp, float _speed, int _damage ,int _dfs,float _aps)      
    {
        Hp = _hp;
        Speed = _speed;
        Damage = _damage;
        DFS = _dfs;
        APS = _aps;
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
        if (amount < DFS) return;
        
        Hp -= amount - DFS;
    }
    public void Heal(int amount) // 체력 회복
    {
        if (Hp + amount > MaxHp)
        {
            Hp = MaxHp;
        }
        else
        {
            Hp += amount;
        }
    }
    public void DamageUP(int amount) //데미지업
    {
        Damage += amount;
    }
    public void DamageDOWN(int amount) //데미지 다운
    {
        Damage -= amount;
    }
    public void APSUP(float amount) // 공속업
    {
        APS += amount;
    }
    public void APSDOWN(float amount) // 공속 다운
    {
        APS -= amount;
    }
    public void DFSUP(int amount)//방어력업
    {
        DFS += amount;
    }
    public void DFSDown(int amount)//방어력다운
    {
        DFS -= amount;
    }
    public void SpeedUP(float amount)//스피드업
    {
        Speed += amount;
    }
    public void SpeedDown(float amount)//스피드 다운
    {
        Speed -= amount;
    }
    public void ItemStateUP(ItemData itemData) //장비 착용시 능력치 업
    {
        DamageUP(itemData.attackPower);
        APSUP(itemData.attackSpeed);
        DFSUP(itemData.defensePower);
        SpeedUP(itemData.Speed);
    }
    public void ItemStateDown(ItemData itemData) //장비 해제시 능력치 다운
    {
        DamageDOWN(itemData.attackPower);
        APSDOWN(itemData.attackSpeed);
        DFSDown(itemData.defensePower);
        SpeedDown(itemData.Speed);
    }
}
