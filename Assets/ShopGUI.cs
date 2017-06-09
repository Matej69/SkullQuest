using UnityEngine;
using System.Collections;


public class ShopGUI : MonoBehaviour {

    static public ShopGUI refrence;

    public Sprite spr_barFilled;
    public Sprite spr_barEmpty;

    public GameObject content;

    public float visibilityRadius = 5f;
    public GameObject shopKeeper;

    GameObject player;

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
        HandleShopVisibility();
    }



    static public void SetVisibility(bool _state)
    {
        if (_state == true)
            refrence.content.SetActive(true);
        else
            refrence.content.SetActive(false);
    }

    void HandleShopVisibility()
    {
        if (Vector2.Distance(player.transform.position, shopKeeper.transform.position) < visibilityRadius)
            SetVisibility(true);
        else
            SetVisibility(false);        
    }






}
