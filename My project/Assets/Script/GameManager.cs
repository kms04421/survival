using UnityEngine;

namespace MainSSM
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;      
        public Transform PlayerPosition { get; private set; } 

        [HideInInspector]public RoundManager roundManager;

        private UIManager UIManager;
        public int monstersKilled = 0;

        public static GameManager Instance
        {

            get
            {
                if (instance == null)
                {
                    return null;
                }
                return instance;
            }

        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            UIManager = GetComponent<UIManager>();
            roundManager = new RoundManager();
            PlayerPosition = GameObject.Find("Player").transform;
            roundManager.SetRoundParameters();// 시작시 몬스터 수량을 위해 추가

        }

       
        public void IncreaseMonstersKilled() // 몬스터 킬카운터 추가
        {
            monstersKilled++;
            if (monstersKilled == roundManager.EnemyCount)
            {                
                monstersKilled = 0;
            }
        }

        


    }
}