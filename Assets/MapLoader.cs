using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MapLoader : MonoBehaviour {

    static public MapLoader refrence;
    
    [Header("GRASS")]
    public List<GameObject> grassChunks;
    static public float grassAppearRadius = 40;
    [Header("ENEMIES")]
    public List<GameObject> enemyAreas;
    public float enemyAppearRadius = 10;

    private GameObject player;

	void Awake()
	{
        refrence = this;
    }
	
	void Start () 
	{
        player = CharacterStateController.refrence.gameObject;
    }

	void Update () 
	{
        HandleGrassChunksLoading();
        HandleEnemyLoading();
        HandleAreaVisibility();
    }



    //GRASS LOADING
    private void HandleGrassChunksLoading()
    {
        foreach (GameObject chunk in grassChunks)        
            if (Vector2.Distance(chunk.transform.position, player.transform.position) < grassAppearRadius)
                chunk.SetActive(true);
            else
                chunk.SetActive(false);
    }

    //ENEMY LOADING
    private void HandleEnemyLoading()
    {
        foreach (GameObject area in enemyAreas)
        {
            EnemyArea areaScr = area.GetComponent<EnemyArea>();
            bool atLeast1EnemyAroundPlayer = false;
            foreach (AEnemy enemy in areaScr.enemies)
                if (Vector2.Distance(enemy.transform.position, player.transform.position) < grassAppearRadius)
                    atLeast1EnemyAroundPlayer = true;
            if(atLeast1EnemyAroundPlayer)
                area.SetActive(true);
        } 
    }

    public void DisableEnemyArea(GameObject _areaObj)
    {
        _areaObj.SetActive(false);
    }
      


    private void HandleAreaVisibility()
    {
        //if at least one enemy is in player visibility radius, increase visibility range
        foreach (GameObject area in enemyAreas)
        {
            if (area.active)
            {
                EnemyArea areaScr = area.GetComponent<EnemyArea>();
                if (areaScr.isAreaActive)
                {
                    float newOpacity = areaScr.ChangeAreaOpacity(-3f);                                  
                }
            }
        }
    }


}
