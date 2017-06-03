using UnityEngine;
using System.Collections;


public class CharacterMovementController : MonoBehaviour {

    public static CharacterMovementController refrence;

    public float speed = 2;

	void Awake()
	{
        refrence = this;
    }
	
	void Start () 
	{
    }

	void Update () 
	{
        HandleMovement();
    }



    void HandleMovement()
    {
        transform.Translate(Joystick.refrence.GetDir() * speed * Time.deltaTime,Space.World);
    }

}
