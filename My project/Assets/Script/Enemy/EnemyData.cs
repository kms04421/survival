using System.Runtime.InteropServices;

namespace MainSSM
{
    [System.Serializable]
    public class EnemyData 
    {
        public int HP;
        public float Speed;
        public int Damage;

        public string name;
        public string Type;

        public EnemyData(EnemyData data)
        {
            HP = data.HP;
            Speed = data.Speed;
            Damage = data.Damage;
            Type = data.Type;

        }
        public void TakeDamage(int amount) //hp ������ ����
        {
            HP -= amount;
        }
        public void HpSet()//���� hp����
        {
            int CurrentHp = GameManager.Instance.roundManager.GetMonsterHPForCurrentRound();// ���� ���庰 hp
            HP = CurrentHp;
        
        }
    }

}