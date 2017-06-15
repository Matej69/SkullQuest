using UnityEngine;
using System.Collections;


public class Slash : ABullet {

    public override void OnStart()
    {
        AudioManager.CreateSound(AudioManager.E_SOUND.SLASH);
    }




}
