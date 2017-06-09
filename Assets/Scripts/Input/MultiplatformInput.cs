using UnityEngine;
using System.Collections;

public class MultiplatformInput : MonoBehaviour {

    public enum E_INTERACTION_TYPE
    {
        MOUSE,
        FINGER
    }
    static public E_INTERACTION_TYPE interactionType = E_INTERACTION_TYPE.FINGER;

    

    public static bool GetInput()
    {
        if (interactionType == E_INTERACTION_TYPE.MOUSE && Input.GetMouseButton(0))
            return true;
        else if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary))
            return true;
        return false;
    }

    public static bool GetInputDown()
    {
        if (interactionType == E_INTERACTION_TYPE.MOUSE && Input.GetMouseButtonDown(0))
            return true;
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            return true;
        return false;
    }

    public static bool GetInputUp()
    {
        if (interactionType == E_INTERACTION_TYPE.MOUSE && Input.GetMouseButtonUp(0))
            return true;
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            return true;
        return false;
    }

    public static Vector2 GetInputPos()
    {
        if (interactionType == E_INTERACTION_TYPE.MOUSE)
            return Input.mousePosition;
        else if (Input.touchCount > 0)
        {
            return Input.GetTouch(0).position;
        }
        Debug.LogError("GetInputPos called but no inputPos could be found");
        return new Vector2(777,777);
    }









}
