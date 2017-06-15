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
    [HideInInspector]
    public Bounds mapBounds;

    private GameObject player;

	void Awake()
	{
        refrence = this;
        CalculateMapBounds();
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



    void CalculateMapBounds()
    {
        List<GameObject> grass = MapLoader.refrence.grassChunks;
        for (int i = 0; i < grass.Count; ++i)
        {
            Vector2 grassPos = grass[i].transform.position;
            //set initial values
            if (i == 0)
            {
                mapBounds.min = new Vector2(grassPos.x, grassPos.y);
                mapBounds.max = new Vector2(grassPos.x, grassPos.y);
            }

            //set new map bound values for min values
            if (grassPos.x < mapBounds.min.x)
                mapBounds.min = new Vector2(grassPos.x, mapBounds.min.y);
            if (grassPos.y < mapBounds.min.y)
                mapBounds.min = new Vector2(mapBounds.min.x, grassPos.y);
            //set new map bound values for max values
            if (grassPos.x > mapBounds.max.x)
                mapBounds.max = new Vector2(grassPos.x, mapBounds.max.y);
            if (grassPos.y > mapBounds.max.y)
                mapBounds.max = new Vector2(mapBounds.max.x, grassPos.y);
        }
    }

    public bool IsInsideMapBounds(GameObject _go)
    {
        Vector2 pos = _go.transform.position;
        if (pos.x > mapBounds.min.x && pos.x < mapBounds.max.x && pos.y > mapBounds.min.y && pos.y < mapBounds.max.y)
            return true;
        return false;         
    }


}
