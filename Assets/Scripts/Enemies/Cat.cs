using UnityEngine;
using System.Collections;


public class Cat : AEnemy {

    
    private Timer timer_dashEvery;
    public float sec_dashEvery;
       
    public override void OnStart()
    {
        canBeRotated = false;
        timer_dashEvery = new Timer(sec_dashEvery);
        timer_dashEvery.currentTime = 0;
    }


    override public void HandleAttack()
    {
        //follow player        
        timer_dashEvery.Tick(Time.deltaTime);
        if (timer_dashEvery.IsFinished())
        {
            attackDir = (player.transform.position - transform.position).normalized;
            moveDir = attackDir;
            timer_dashEvery.Reset();            
        }

        //apply movement
        transform.Translate(attackDir * attackSpeed * Time.deltaTime, Space.World);

        
        //rotate
        int scaleX = ((attackDir * attackSpeed).x >= 0) ? 1 : -1;
        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        

        //apply damage
        if (timer_attack.IsFinished() && IsTouchingPlayer())
        {
            CharacterStateController.refrence.TryReduceHealthOnDamage(attackDamage);
            timer_attack.Reset();
        }
    }

    override public void HandleMovement()
    {
        //change direction every X seconds
        timer_changeMoveDir.Tick(Time.deltaTime);
        if (timer_changeMoveDir.IsFinished())
        {
            moveDir = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f)).normalized;
            timer_changeMoveDir.Reset();
        }

        //apply movement
        transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
        //rotate        
        int scaleX = ((moveDir * moveSpeed).x >= 0) ? 1 : -1;
        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        
    }
    


}
