
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
            roundManager.SetRoundParameters();// 시작시 몬스터 수량을 위해 추가
            InitializeObjects();
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

        public void InitializeObjects()//  오브젝트 초기화
        {
            foreach (GameObject obj in initialObjects)
            {
                obj.SetActive(true);
                obj.SetActive(false);
            }
        }

        public void QuitGame()//게임종료
        {
            Application.Quit();
        }

        public void RestartGame()
        {
            // 현재 씬 다시 로드
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}