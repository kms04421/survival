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
            roundManager.SetRoundParameters();// ���۽� ���� ������ ���� �߰�

        }

       
        public void IncreaseMonstersKilled() // ���� ųī���� �߰�
        {
            monstersKilled++;
            if (monstersKilled == roundManager.EnemyCount)
            {                
                monstersKilled = 0;
            }
        }

        


    }
}