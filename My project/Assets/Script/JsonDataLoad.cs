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
            // Resources �������� JSON ���� �ε� (��: Resources/Enemies/enemyData.json)
            TextAsset jsonFile = Resources.Load<TextAsset>("enemyData");
            if (jsonFile == null)
            {
                Debug.LogError("JSON ������ �������� �ʽ��ϴ�.");
                return;
            }
         
            // JSON ������ �Ľ��Ͽ� ���� List<Enemy>�� ��ȯ
            EnemyListWrapper wrapper = JsonUtility.FromJson<EnemyListWrapper>(jsonFile.text);

            foreach(EnemyData data in wrapper.EnemyList)
            {
                enemies.Add(data.name, data);
            }


        }


        // Enemy ����� �����ϴ� Ŭ����
        [System.Serializable]
        public class EnemyListWrapper
        {
            public List<EnemyData> EnemyList;
        }


    }
}
