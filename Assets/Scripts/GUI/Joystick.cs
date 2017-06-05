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
        Vector2 touchPos = Vector2.zero;
        if (IsFingerInsideJoystickCol(ref touchPos))        
            return (touchPos - joystickPos).normalized;        
        else
            return Vector2.zero;        
    }

    //must be like this for multiple touches
    public bool IsFingerInsideJoystickCol(ref Vector2 _insideTouchpos)
    {
        if (MultiplatformInput.interactionType == MultiplatformInput.E_INTERACTION_TYPE.FINGER)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                Vector2 touchPos = Input.touches[i].position;
                if (boxCollider.bounds.Contains(touchPos))
                {
                    _insideTouchpos = touchPos;
                    return true;
                }
            }
            return false;
        }
        else
        {
            Vector2 mousePos = MultiplatformInput.GetInputPos();
            if (boxCollider.bounds.Contains(mousePos))
            {
                _insideTouchpos = mousePos;
                return true;
            }
            else
                return false;
        }
    }


}
