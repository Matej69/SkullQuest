using UnityEngine;
using System.Collections;


public class GameManager : MonoBehaviour {

    public static GameManager refrence;

    int bossesKilled = 0;
    int numOfBossess = 2;

    GameManager()
    {
        refrence = this;
    }
    
	
	void Start () 
	{
        GameScenes.TriggerScene(GameScenes.E_SCENE.START);
	}

	void Update () 
	{	
	}



    public void EnableGameScreen()
    {
        gameObject.SetActive(true);        
    }



    public bool AreAllBossessKilled()
    {
        return (numOfBossess == bossesKilled);
    }
    public void IncreaseDeadBossessCount()
    {
        bossesKilled++;
        if (AreAllBossessKilled())
            GameScenes.TriggerScene(GameScenes.E_SCENE.END);
    }






}
