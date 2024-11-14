using UnityEngine;

namespace MainSSM
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        public Transform PlayerPosition { get; private set; } 

        [HideInInspector]public RoundManager roundManager;

        [HideInInspector]public int monstersKilled = 0;

    
        private void Awake()
        {          
            roundManager = new RoundManager();
            PlayerPosition = GameObject.Find("Player").transform;
            roundManager.SetRoundParameters();// 시작시 몬스터 수량을 위해 추가
        }

       
        public void IncreaseMonstersKilled() // 몬스터 킬카운터 추가
        {
            monstersKilled++;
           
            UIManager.Instance.monsterTotalAmount.text = (roundManager.EnemyCount - monstersKilled).ToString();
            if (monstersKilled == roundManager.EnemyCount)
            {
                UIManager.Instance.ActivateTimer();
                monstersKilled = 0;
            }
        }

        


    }
}