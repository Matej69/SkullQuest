using UnityEngine;
using System.Collections;


public class CharacterMovementController : MonoBehaviour {

    public static CharacterMovementController refrence;
    public Timer timer_stunned;
    public float speed = 2;

    CharacterStateController playerState;

    void Awake()
	{
        refrence = this;        
    }
	
	void Start () 
	{
        playerState = CharacterStateController.refrence;
        timer_stunned = new Timer(0f);        
    }

	void Update () 
	{
        timer_stunned.Tick(Time.deltaTime);
        if(timer_stunned.IsFinished() && !playerState.respawning)
            HandleMovement();
        if (playerState.respawning)
            HandleRespawning();
        
    }
    



    void HandleMovement()
    {
        Vector3 prevPos = transform.position;
        transform.Translate(Joystick.refrence.GetDir() * speed * Time.deltaTime,Space.World);
        //if we are outside bounds after moving, cancel that move
        if (!MapLoader.refrence.IsInsideMapBounds(gameObject))
            transform.position = prevPos;
    }

    void HandleRespawning()
    {
        transform.position = Vector2.Lerp(transform.position, ShopGUI.refrence.shopKeeper.transform.position, 1f * Time.deltaTime);
        if (Vector2.Distance(transform.position, ShopGUI.refrence.shopKeeper.transform.position) < 4f)
        {
            playerState.respawning = false;
            playerState.ResetHealth();
        }
        
    }

}
