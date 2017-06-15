using UnityEngine;
using System.Collections;


public class Eye : AEnemy {


    public override void OnStart()
    {
        canBeRotated = true;
    }



    override public void HandleAttack()
    {  
        //change direction every X seconds
        if (timer_changeAttackDir.IsFinished())
        {
            attackDir = (player.transform.position - transform.position).normalized;
            moveDir = attackDir;
            float rotationAngle = Global.GetAngle(Vector2.right, attackDir);
            transform.eulerAngles = new Vector3(0, 0, rotationAngle);
            timer_changeAttackDir.Reset();
        }
        
        //if player is reached inflict damage and reset attack timer
        if(IsTouchingPlayer())
        {
            //reset direction change -> will make enemy wait for X seconds
            timer_changeAttackDir.Reset();
            //deal damage
            if (timer_attack.IsFinished())
            {
                CharacterStateController.refrence.TryReduceHealthOnDamage(attackDamage);
                timer_attack.Reset();
            }
        }        
        
        //apply movement
        //if(!IsTouchingPlayer())
        transform.Translate(attackDir * attackSpeed * Time.deltaTime, Space.World);        
    }

    override public void HandleMovement()
    {
        //change direction every X seconds
        if(timer_changeMoveDir.IsFinished())
        {
            moveDir = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f)).normalized;
            float rotationAngle = Global.GetAngle(Vector2.right, moveDir);
            transform.eulerAngles = new Vector3(0, 0, rotationAngle);
            timer_changeMoveDir.Reset();
        }

        //apply movement
        transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
    }


}
