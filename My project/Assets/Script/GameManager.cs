
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
namespace MainSSM
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        public Transform PlayerPosition { get; private set; } 

        [HideInInspector]public RoundManager roundManager;

        [HideInInspector]public int monstersKilled = 0;

        public List<GameObject> initialObjects;
        private void Awake()
        {          
            roundManager = new RoundManager();
            PlayerPosition = GameObject.Find("Player").transform;
            roundManager.SetRoundParameters();// ���۽� ���� ������ ���� �߰�
            InitializeObjects();
        }

       
        public void IncreaseMonstersKilled() // ���� ųī���� �߰�
        {
            monstersKilled++;
           
            UIManager.Instance.monsterTotalAmount.text = (roundManager.EnemyCount - monstersKilled).ToString();
            if (monstersKilled == roundManager.EnemyCount)
            {
                UIManager.Instance.ActivateTimer();
                monstersKilled = 0;
            }
        }

        public void InitializeObjects()//  ������Ʈ �ʱ�ȭ
        {
            foreach (GameObject obj in initialObjects)
            {
                obj.SetActive(true);
                obj.SetActive(false);
            }
        }

        public void QuitGame()//��������
        {
            Application.Quit();
        }

        public void RestartGame()
        {
            // ���� �� �ٽ� �ε�
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}