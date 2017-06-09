using UnityEngine;
using System.Collections;


public class ShopGUI : MonoBehaviour {

    static public ShopGUI refrence;

    public Sprite spr_barFilled;
    public Sprite spr_barEmpty;

    public GameObject content;

	void Awake()
	{
        refrence = this;
    }
	
	void Start () 
	{	
	}

	void Update () 
	{	
	}



    static public void SetVisibility(bool _state)
    {
        if (_state == true)
            refrence.content.SetActive(true);
        else
            refrence.content.SetActive(false);
    }






}
