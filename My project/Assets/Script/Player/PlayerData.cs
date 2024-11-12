using MainSSM;

public class PlayerData : ICharacterData
{
    public int Hp { get; set; }
    public float Speed { get; set; }
    public int Damage { get; set; }
    public int MaxHp { get; set; }
    public PlayerData(int hp, float speed, int damage)      
    {
        Hp = hp;
        Speed = speed;
        Damage = damage;
        MaxHp = hp;
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
        Hp -= amount;
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
}
