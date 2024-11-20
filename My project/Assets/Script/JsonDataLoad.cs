using System.Collections.Generic;
using UnityEngine;
namespace MainSSM
{

    public class JsonDataLoad : MonoBehaviour
    {
        public EnemyData enemyData;
        public static Dictionary<string,EnemyData> enemies;
      
        private void Awake()
        {
            enemies = new Dictionary<string, EnemyData> ();
            LoadEnemiesFromJSON();
        }

        private void LoadEnemiesFromJSON()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("enemyData");
            if (jsonFile == null)
            {
                Debug.LogError("json없음");
                return;
            }     
            EnemyListWrapper wrapper = JsonUtility.FromJson<EnemyListWrapper>(jsonFile.text); // JSON 파일을 파싱하여 List<Enemy>로 변환
            foreach (EnemyData data in wrapper.EnemyList)
            {
                enemies.Add(data.name, data);
            }

        }
        [System.Serializable]
        public class EnemyListWrapper
        {
            public List<EnemyData> EnemyList;
        }


    }
}
