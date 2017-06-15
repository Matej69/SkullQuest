using UnityEngine;
using System.Collections;


public class FreeAudio : MonoBehaviour {

	void Awake()
	{
	}
	
	void Start () 
	{	
	}

	void Update ()
    {
        if (!GetComponent<AudioSource>().isPlaying)
            Destroy(gameObject);
	}
}
