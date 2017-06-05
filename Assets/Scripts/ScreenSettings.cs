using UnityEngine;
using System.Collections;


public class ScreenSettings : MonoBehaviour {

	void Awake()
	{
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
	
}
