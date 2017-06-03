using UnityEngine;
using System.Collections;


public class Global : MonoBehaviour {

    static public float GetAngle(Vector2 _v1, Vector2 _v2)
    {
        float sign = Mathf.Sign(_v1.x * _v2.y - _v1.y * _v2.x);
        return Vector2.Angle(_v1, _v2) * sign;
    }
}
