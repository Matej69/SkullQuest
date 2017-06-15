using UnityEngine;
using System.Collections;


public class Web : ABullet {

    [Header("Web only")]
    public float stunTime;


    override public void OnHitPlayer()
    {
        base.OnHitPlayer();
        speed = 0;
        CharacterMovementController.refrence.timer_stunned = new Timer(stunTime);
    }

}
