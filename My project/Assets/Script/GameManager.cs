using UnityEngine;

namespace MainSSM
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;      
        public Transform PlayerPosition { get; private set; } // 

        public RoundManager roundManager;

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
            roundManager = new RoundManager();
            PlayerPosition = GameObject.Find("Player").transform;
            roundManager.SetRoundParameters();// ���۽� ���� ������ ���� �߰�

        }
        public void IncreaseMonstersKilled() // ���� ųī���� �߰�
        {
            monstersKilled++;
            if (monstersKilled == roundManager.EnemyCount)
            {
                roundManager.NextRound();
                monstersKilled = 0;
            }
        }

        


    }
}