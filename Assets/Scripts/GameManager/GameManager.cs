using UnityEngine;
using System.Collections;


public class GameManager : MonoBehaviour {

    public static GameManager refrence;

    GameManager()
    {
        refrence = this;
    }
    
	
	void Start () 
	{	
	}

	void Update () 
	{	
	}


    public void EnableGameScreen()
    {
        gameObject.SetActive(true);        
    }






}
