namespace MainSSM
{
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

        public void TakeDamage(int amount) //hp 데미지 적용
        {
            Hp -= amount;
        }
        public void HpSet()//몬스터 hp셋팅
        {
            Hp = GameManager.Instance.roundManager.GetMonsterHPForCurrentRound();// 몬스터 라운드별 hp
        }
    }

}