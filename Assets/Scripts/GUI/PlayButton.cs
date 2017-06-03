using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayButton : MonoBehaviour {

	
	void Start () 
	{
        SetOnClickListener();
    }


    void SetOnClickListener()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            GameManager.refrence.EnableGameScreen();
            gameObject.SetActive(false);
        });
    }
	




}
