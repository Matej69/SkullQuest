using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyArea : MonoBehaviour {

    public int numOfEnemies;
    public AEnemy.E_ID id;
    public float spawnRadius;
    
    List<AEnemy> enemies = new List<AEnemy>();
    
    private GameObject player;

	void Awake()
	{  
    }
	
	void Start () 
	{
        player = CharacterStateController.refrence.gameObject;
        SpawnEnemies();
    }

	void Update () 
	{
        HandleLife();
    }


    private void SpawnEnemies()
    {
        for(int i = 0; i < numOfEnemies; ++i)
        {
            Vector2 startPos = new Vector2(transform.position.x + Random.Range(-spawnRadius, spawnRadius), transform.position.y + Random.Range(-spawnRadius, spawnRadius));
            enemies.Add(EnemyFactory.CreateEnemy(id, startPos).GetComponent<AEnemy>());
        }                                 
    }

    private void HandleLife()
    {
        
        for (int i = enemies.Count - 1; i >= 0; --i)
        {
            //change direction of ones that are outside area range
            if (enemies[i] != null)
            {
                if (Vector2.Distance(enemies[i].transform.position, transform.position) > spawnRadius && enemies[i].activity == AEnemy.E_ACTIVITY.IDLE)
                {
                    enemies[i].moveDir = (transform.position - enemies[i].transform.position).normalized;
                    //rotate only ones that need to be rotated
                    if (enemies[i].canBeRotated)
                    {
                        float rotationAngle = Global.GetAngle(Vector2.right, enemies[i].moveDir);
                        enemies[i].transform.eulerAngles = new Vector3(0, 0, rotationAngle);
                    }
                }
            }

            //remove death ones
            if (enemies[i] == null)
                enemies.Remove(enemies[i]);
        }

        //if far enough, spawn new ones
        if (Vector2.Distance(transform.position, player.transform.position) > spawnRadius + 14f)
        {
            while (enemies.Count < numOfEnemies)
            {
                //create new enemy
                Vector2 startPos = new Vector2(transform.position.x + Random.Range(-spawnRadius, spawnRadius), transform.position.y + Random.Range(-spawnRadius, spawnRadius));
                enemies.Add(EnemyFactory.CreateEnemy(id, startPos).GetComponent<AEnemy>());
            }
        }
    }




}
