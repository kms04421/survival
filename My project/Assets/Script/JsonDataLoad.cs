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
            // Resources 폴더에서 JSON 파일 로드 (예: Resources/Enemies/enemyData.json)
            TextAsset jsonFile = Resources.Load<TextAsset>("enemyData");
            if (jsonFile == null)
            {
                Debug.LogError("JSON 파일이 존재하지 않습니다.");
                return;
            }
         
            // JSON 파일을 파싱하여 직접 List<Enemy>로 변환
            EnemyListWrapper wrapper = JsonUtility.FromJson<EnemyListWrapper>(jsonFile.text);

            foreach(EnemyData data in wrapper.EnemyList)
            {
                enemies.Add(data.name, data);
            }


        }


        // Enemy 목록을 래핑하는 클래스
        [System.Serializable]
        public class EnemyListWrapper
        {
            public List<EnemyData> EnemyList;
        }


    }
}
