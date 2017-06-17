using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class AudioGUI : MonoBehaviour {

    public Sprite spr_muted;
    public Sprite spr_notMuted;

    void Awake()
	{
	}
	
	void Start () 
	{
        SetOnClickListener();
    }

	void Update () 
	{	
	}


    void SetOnClickListener()
    {
        GetComponent<Button>().onClick.AddListener(delegate{
            if (AudioManager.muted)
            {
                GetComponent<Button>().image.sprite = spr_notMuted;
                AudioManager.SetMuted(false);
            }
            else
            {
                GetComponent<Button>().image.sprite = spr_muted;
                AudioManager.SetMuted(true);
            }
        });
    }



}
