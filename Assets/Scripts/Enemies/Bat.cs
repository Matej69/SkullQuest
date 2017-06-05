using UnityEngine;
using System.Collections;


public class Bat : AEnemy {

    public override void OnStart()
    {
        canBeRotated = false;
        attackSpeed = Random.Range(attackSpeed - 0.5f, attackSpeed + 0.5f);
    }
    

    override public void HandleAttack()
    {
        //follow player
        Vector2 newPos = Vector2.Lerp(transform.position, player.transform.position, attackSpeed * Time.deltaTime);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);

        //apply damage
        if (timer_attack.IsFinished() && IsTouchingPlayer())
        {
            CharacterStateController.refrence.ReduceHealth(attackDamage);
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
    }
    


}
