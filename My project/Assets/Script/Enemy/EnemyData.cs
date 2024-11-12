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
        public void TakeDamage(int amount) //hp 데미지 적용
        {
            HP -= amount;
        }
        public void HpSet()//몬스터 hp셋팅
        {
            int CurrentHp = GameManager.Instance.roundManager.GetMonsterHPForCurrentRound();// 몬스터 라운드별 hp
            HP = CurrentHp;
        
        }
    }

}