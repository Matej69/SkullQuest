using UnityEngine;
using System.Collections;


public class SmallSpider : AEnemy {

    Timer timer_dieAfter;
    [Header("Small spider only")]
    public float dieAfter;

    public override void OnStart()
    {
        timer_dieAfter = new Timer(dieAfter);
    }

    override public void HandleAttack()
    {
        FollowPlayer();
        HandleDeathAfter();
        //apply damage
        if (timer_attack.IsFinished() && IsTouchingPlayer())
        {
            CharacterStateController.refrence.ReduceHealth(attackDamage);
            timer_attack.Reset();
        }
    }

    override public void HandleMovement()
    {
        FollowPlayer();
        HandleDeathAfter();
    }

    private void FollowPlayer()
    {
        
        if (Vector2.Distance(transform.position, player.transform.position) > 0.05f)
        {
            //change direction
            attackDir = (player.transform.position - transform.position).normalized;
            moveDir = attackDir;
            float rotationAngle = Global.GetAngle(Vector2.right, attackDir);
            transform.eulerAngles = new Vector3(0, 0, rotationAngle);

            //apply movement
            transform.Translate((player.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime, Space.World);
        }        
    }

    private void HandleDeathAfter()
    {
        timer_dieAfter.Tick(Time.deltaTime);
        if(timer_dieAfter.IsFinished())
        {
            health = 0;
        }
    }
    

}
