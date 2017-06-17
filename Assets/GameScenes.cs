using UnityEngine;
using System.Collections;


public class GameScenes : MonoBehaviour {

    private static GameScenes refrence;

    public GameObject startScene;
    public GameObject endScene;

    private Timer timer_startScene;
    private Timer timer_endScene;

    public GameObject mainCanvas;

    public enum E_SCENE{ START, END, NONE };
    E_SCENE activeScene = E_SCENE.NONE;



    void Awake()
    {
        refrence = this;
        timer_startScene = new Timer(6f);
        timer_endScene = new Timer(12f);
    }

    void Update()
    {
        if(activeScene == E_SCENE.START)
        {
            timer_startScene.Tick(Time.deltaTime);
            if(timer_startScene.IsFinished())
            {
                activeScene = E_SCENE.NONE;
                Destroy(startScene);
                GameManager.refrence.gameObject.SetActive(true);
                refrence.mainCanvas.SetActive(true);
            }
        }     
        /*      
        else if(activeScene == E_SCENE.END)
        {
            timer_endScene.Tick(Time.deltaTime);
            if (timer_endScene.IsFinished())
            {
                Destroy(endScene);
            }
        } 
        */           
    }


    static public void TriggerScene(E_SCENE _sceneId)
    {
        refrence.activeScene = _sceneId;

        if (_sceneId == E_SCENE.START)
        {            
            refrence.startScene.SetActive(true);            
        }
        else if (_sceneId == E_SCENE.END)
        {
            Camera.main.transform.position = new Vector3(refrence.endScene.transform.position.x, refrence.endScene.transform.position.y, Camera.main.transform.position.z);
            refrence.endScene.SetActive(true);
            refrence.mainCanvas.SetActive(false);
            GameManager.refrence.gameObject.SetActive(false);                     
        }
    }







    
}
