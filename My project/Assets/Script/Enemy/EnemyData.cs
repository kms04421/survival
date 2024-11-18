using System.Runtime.InteropServices;

namespace MainSSM
{
    [System.Serializable]
    public class EnemyData 
    {
        public int HP;
        public float Speed;
        public float Damage;
        public float Power;
        public string name;
        public string Type;

        public EnemyData(EnemyData data)
        {
            HP = data.HP;
            Speed = data.Speed;
            Damage = data.Damage;
            Power = data.Damage;
            Type = data.Type;

        }
        public void TakeDamage(float amount) //hp ������ ����
        {
            HP -= (int)amount;
        }
        public void MonstrRoundDataSet()//���� RoundData����
        {
            if (GameManager.Instance == null) return;
            int CurrentHp = GameManager.Instance.roundManager.GetMonsterHPForCurrentRound();// ���� ���庰 hp
            Damage = GameManager.Instance.roundManager.GetMonsterPowerForCurrentRound() + Power; // 
            HP = CurrentHp;
        
        }
    }

}