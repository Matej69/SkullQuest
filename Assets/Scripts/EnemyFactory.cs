using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyFactory : MonoBehaviour {

    static private EnemyFactory refrence;

    [System.Serializable]
    public class EnemyInfo
    {
        public AEnemy.E_ID id;
        public GameObject prefab;
    }
    public List<EnemyInfo> enemies = new List<EnemyInfo>();



	void Awake()
	{
        refrence = this;
    }


    static public GameObject CreateEnemy(AEnemy.E_ID _id, Vector2 _pos)
    {
        foreach (EnemyInfo enemyInfo in refrence.enemies)
            if (enemyInfo.id == _id)
            {
                GameObject enemy = (GameObject)Instantiate(enemyInfo.prefab, new Vector3(_pos.x, _pos.y, enemyInfo.prefab.transform.position.z), Quaternion.identity);
                return enemy;
            }

        return null;
    }
	




}
