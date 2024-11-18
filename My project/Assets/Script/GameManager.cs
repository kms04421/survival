
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

        private RandomStateUi randomStateUi;
        private void Awake()
        {
            randomStateUi = FindAnyObjectByType<RandomStateUi>();   
            roundManager = new RoundManager();
            PlayerPosition = GameObject.Find("Player").transform;
            roundManager.SetRoundParameters();// ���۽� ���� ������ ���� �߰�
            InitializeObjects();
            Time.timeScale = 0f;//���� �Ͻ�����
        }

       
        public void IncreaseMonstersKilled() // ���� ųī���� �߰�
        {
            monstersKilled++;
           
            UIManager.Instance.monsterTotalAmount.text = (roundManager.EnemyCount - monstersKilled).ToString();
            if (monstersKilled == roundManager.EnemyCount)
            {
                randomStateUi.SetRandomState();
                AudioManager.Instance.BackGroundAudioPlay(1);
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

        public void StartGame()
        {
            Time.timeScale = 1f;
        }
    }
}