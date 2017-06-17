using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CameraController : MonoBehaviour {

    public float followSpeed = 4;
    public float deltaFrontDistance;

    private bool insideX = false;
    private bool insideY = false;

    void Awake()
	{
	}
	
	void Start () 
	{
        
    }

	void Update () 
	{
        if(GameManager.refrence.gameObject.active)
            HandlePlayerFollow();
    }



    void HandlePlayerFollow()
    {
        Vector2 playerPos = CharacterMovementController.refrence.transform.position;
        Vector2 addDist = deltaFrontDistance * Joystick.refrence.GetDir() * Time.deltaTime;
        Vector2 newPos = Vector2.Lerp((Vector2)transform.position + addDist, playerPos, followSpeed * Time.deltaTime);
        SetProperCamPos(ref newPos, transform.position);
        transform.position = new Vector3(newPos.x, newPos.y, -10);
    }

    
    void SetProperCamPos(ref Vector2 _futurePos, Vector2 _prevPos)
    {
        Vector2 pos = _futurePos;
        Bounds mapBounds = MapLoader.refrence.mapBounds;
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        float screenW = ToWorld(ToScreen(pos) + screenSize / 2).x - ToWorld(ToScreen(pos) - screenSize / 2).x;
        float screenH = ToWorld(ToScreen(pos) + screenSize / 2).y - ToWorld(ToScreen(pos) - screenSize / 2).y;
        screenSize = Camera.main.ScreenToWorldPoint(screenSize);
        _futurePos.x = (pos.x - screenW / 2 > mapBounds.min.x && pos.x + screenW / 2 < mapBounds.max.x) ? _futurePos.x : _prevPos.x;
        _futurePos.y = (pos.y - screenH / 2 > mapBounds.min.y && pos.y + screenH / 2 < mapBounds.max.y) ? _futurePos.y : _prevPos.y;
    }

    private Vector2 ToWorld(Vector2 _pos)
    {
        return Camera.main.ScreenToWorldPoint(_pos);
    }
    private Vector2 ToScreen(Vector2 _pos)
    {
        return Camera.main.WorldToScreenPoint(_pos);
    }





}
