using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Joystick : MonoBehaviour {

    static public Joystick refrence;
    
    Vector2 joystickPos;
    BoxCollider2D boxCollider;

    void Awake()
	{
        refrence = this;
        boxCollider = GetComponent<BoxCollider2D>();
    }
	
	void Start () 
	{
        joystickPos = GetComponent<Image>().transform.position;                
    }

    void Update()
    {
        //if resoultion changes it will still get proper iamge position
        joystickPos = GetComponent<Image>().transform.position;
    }
    


    public Vector2 GetDir()
    {
        if (IsFingerInsideJoystickCol())        
            return (MultiplatformInput.GetInputPos() - joystickPos).normalized;        
        else
            return Vector2.zero;        
    }

    public bool IsFingerInsideJoystickCol()
    {        
        Vector2 inputPos = MultiplatformInput.GetInputPos();
        return ((boxCollider.bounds.Contains(inputPos)));
    }


}
