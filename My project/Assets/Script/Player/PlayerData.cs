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
    public void TakeDamage(int amount) //hp ������ ����
    {
        if (amount < DFS) return;
        
        Hp -= amount - DFS;
    }
    public void Heal(int amount) // ü�� ȸ��
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
    public void DamageUP(int amount) //��������
    {
        Damage += amount;
    }
    public void DamageDOWN(int amount) //������ �ٿ�
    {
        Damage -= amount;
    }
    public void APSUP(float amount) // ���Ӿ�
    {
        APS += amount;
    }
    public void APSDOWN(float amount) // ���� �ٿ�
    {
        APS -= amount;
    }
    public void DFSUP(int amount)//���¾�
    {
        DFS += amount;
    }
    public void DFSDown(int amount)//���´ٿ�
    {
        DFS -= amount;
    }
    public void SpeedUP(float amount)//���ǵ��
    {
        Speed += amount;
    }
    public void SpeedDown(float amount)//���ǵ� �ٿ�
    {
        Speed -= amount;
    }
    public void ItemStateUP(ItemData itemData) //��� ����� �ɷ�ġ ��
    {
        DamageUP(itemData.attackPower);
        APSUP(itemData.attackSpeed);
        DFSUP(itemData.defensePower);
        SpeedUP(itemData.Speed);
    }
    public void ItemStateDown(ItemData itemData) //��� ������ �ɷ�ġ �ٿ�
    {
        DamageDOWN(itemData.attackPower);
        APSDOWN(itemData.attackSpeed);
        DFSDown(itemData.defensePower);
        SpeedDown(itemData.Speed);
    }
}
