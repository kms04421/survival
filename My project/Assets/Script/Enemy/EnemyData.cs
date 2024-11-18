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
        public void TakeDamage(float amount) //hp 데미지 적용
        {
            HP -= (int)amount;
        }
        public void MonstrRoundDataSet()//몬스터 RoundData셋팅
        {
            if (GameManager.Instance == null) return;
            int CurrentHp = GameManager.Instance.roundManager.GetMonsterHPForCurrentRound();// 몬스터 라운드별 hp
            Damage = GameManager.Instance.roundManager.GetMonsterPowerForCurrentRound() + Power; // 
            HP = CurrentHp;
        
        }
    }

}