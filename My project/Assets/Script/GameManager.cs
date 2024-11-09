using UnityEngine;

namespace MainSSM
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        public Transform PlayerPosition { get; private set; } 

        [HideInInspector]public RoundManager roundManager;

        private UIManager UIManager;
        public int monstersKilled = 0;

    
        private void Awake()
        {
          
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