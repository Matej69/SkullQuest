using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AudioManager : MonoBehaviour {
    
    private static AudioManager refrence;

    public GameObject pref_freeAudio;
    
    [HideInInspector]
    static public bool muted = false;

    public enum E_SOUND { SLASH, KILL, PURCHASE, PURCHASE_FAILED, COIN }   
    [System.Serializable] 
    public class Sound{
        public E_SOUND id;
        public AudioClip clip;
    }
    public List<Sound> sounds;




	void Awake()
	{
        refrence = this;
    }
	
	void Start () 
	{
        
    }

	void Update () 
	{
        
    }



    static public void CreateSound(E_SOUND _id)
    {
        foreach(Sound sound in refrence.sounds)
            if(sound.id == _id)
            {
                GameObject s = (GameObject)Instantiate(refrence.pref_freeAudio);
                s.GetComponent<AudioSource>().clip = sound.clip;
                s.GetComponent<AudioSource>().pitch = (float)Random.Range(87, 120) / 100;
                s.GetComponent<AudioSource>().volume = (!muted) ? 1 : 0;
                s.GetComponent<AudioSource>().Play();
            }
    }

    static public void SetMuted(bool _state)
    {
        muted = _state;
        refrence.GetComponent<AudioSource>().volume = (!muted) ? 1 : 0;
    }










}
