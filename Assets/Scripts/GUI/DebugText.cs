using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class DebugText : MonoBehaviour {

    private static DebugText refrence;

	void Awake()
	{
        refrence = this;
	}


    static public void WriteText(string _str)
    {
        refrence.GetComponent<Text>().text += _str;
    }






}
