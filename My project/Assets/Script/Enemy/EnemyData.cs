
public class EnemyData : ICharacterData
{
    public int Hp { get; set; }
    public float Speed { get; set; }
    public int Damage { get; set; }

    public EnemyData(int hp, float speed, int damage)
    {
        Hp = hp;
        Speed = speed;
        Damage = damage;
    }
}
