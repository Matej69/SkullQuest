using UnityEngine;
using System.Collections;


public class CameraController : MonoBehaviour {

    public float followSpeed = 1;
    public float deltaFrontDistance;


	void Awake()
	{
	}
	
	void Start () 
	{	
	}

	void Update () 
	{
        HandlePlayerFollow();
    }



    void HandlePlayerFollow()
    {
        Vector2 playerPos = CharacterMovementController.refrence.transform.position;
        Vector2 addDist = deltaFrontDistance * Joystick.refrence.GetDir() * Time.deltaTime;
        Vector2 pos2D = Vector2.Lerp((Vector2)transform.position + addDist, playerPos, followSpeed * Time.deltaTime);
        transform.position = new Vector3(pos2D.x, pos2D.y, -10);
    }



}
