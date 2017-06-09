using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyArea : MonoBehaviour {

    public int numOfEnemies;
    public AEnemy.E_ID id;
    public float spawnRadius;
    private float spawnPlusExtraRadius;

    [HideInInspector]
    public List<AEnemy> enemies = new List<AEnemy>();
    [HideInInspector]
    public float areaOpacity = 1;

    [HideInInspector]
    public bool isAreaActive = true;
    
    private GameObject player;

	void Awake()
	{  
    }
	
	void Start () 
	{
        player = CharacterStateController.refrence.gameObject;
        spawnPlusExtraRadius = spawnRadius + MapLoader.grassAppearRadius;
        SpawnEnemies();
    }

	void Update () 
	{
        HandleLife();
    }



    public float ChangeAreaOpacity(float _speed)
    {
        foreach (AEnemy enemy in enemies)
        {
                float newOpacity = enemy.ChangeOpacity(_speed);
                if (_speed < 0)
                    areaOpacity = (newOpacity > areaOpacity) ? newOpacity : areaOpacity;
                else
                    areaOpacity = (newOpacity < areaOpacity) ? newOpacity : areaOpacity;            
        }
        return areaOpacity;
    }


    private void SpawnEnemies()
    {
        for(int i = 0; i < numOfEnemies; ++i)
        {
            Vector2 startPos = new Vector2(transform.position.x + Random.Range(-spawnRadius, spawnRadius), transform.position.y + Random.Range(-spawnRadius, spawnRadius));
            AEnemy newEnemy = EnemyFactory.CreateEnemy(id, startPos).GetComponent<AEnemy>();
            newEnemy.transform.SetParent(gameObject.transform);
            enemies.Add(newEnemy);

        }                                 
    }
    

    private void HandleLife()
    {
        bool allEnemiesOutsidePlayerVision = true;
        for (int i = enemies.Count - 1; i >= 0; --i)
        {            
            if (enemies[i] != null)
            {
                //IF VISIBLE BY PLAYER
                if (Vector2.Distance(enemies[i].transform.position, player.transform.position) < MapLoader.grassAppearRadius)
                {
                    isAreaActive = true;
                    allEnemiesOutsidePlayerVision = false;                    
                }                

                //IF OUTSIDE SPAWN RADIUS
                if (Vector2.Distance(enemies[i].transform.position, transform.position) > spawnRadius)
                {                    
                    //change direction of ones that are outside area range
                    if (enemies[i].activity == AEnemy.E_ACTIVITY.IDLE)
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
            }

            //remove death ones or one that are dying(dying ones will not bbe part of the area anymore)
            if (enemies[i] == null || enemies[i].isDying)
                enemies.Remove(enemies[i]);
        }
                
        //Disable area if all enemies are inside spawn range and if they're not visible by player
        if (allEnemiesOutsidePlayerVision && Vector2.Distance(transform.position, player.transform.position) > spawnPlusExtraRadius)
        {
            isAreaActive = false;
            if(ChangeAreaOpacity(4f) <= 0)
                MapLoader.refrence.DisableEnemyArea(gameObject);
        }

        //if far enough, spawn new ones
        if (Vector2.Distance(transform.position, player.transform.position) > spawnPlusExtraRadius)
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
