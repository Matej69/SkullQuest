using UnityEngine;
using System.Collections;


public class Ghost : AEnemy {

    public Timer timer_invisibleFor;
    public Timer timer_invisibleEvery;
    public float sec_beInvisibleFor;
    public float sec_beInvisibleEvery;

    float targetAlpha = 1;
    public float invisiblityReduceSpeed = 1;


    public override void OnStart()
    {
        timer_invisibleFor = new Timer(sec_beInvisibleFor);
        timer_invisibleEvery = new Timer(sec_beInvisibleEvery);
        canBeRotated = false;
    }


    override public void HandleAttack()
    {
        //follow player
        Vector2 playerDir = (player.transform.position - transform.position).normalized;
        transform.Translate(playerDir * attackSpeed * Time.deltaTime, Space.World);
        //rotate only if not to close to player(to avoid x,-x,x,-x,x,-x every frame)
        if (Vector2.Distance(transform.position, player.transform.position) > 0.05f)
        {
            int scaleX = ((playerDir * attackSpeed).x > 0) ? 1 : -1;
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        }

        //apply visibility
        targetAlpha = 1;
        HandleVisibilityChange();


        //apply damage
        if (timer_attack.IsFinished() && IsTouchingPlayer())
        {
            CharacterStateController.refrence.TryReduceHealthOnDamage(attackDamage);
            timer_attack.Reset();
        }
    }

    override public void HandleMovement()
    {
        //Manage invisibility
        timer_invisibleEvery.Tick(Time.deltaTime);
        if (timer_invisibleEvery.IsFinished())
        {
            targetAlpha = 0;
            timer_invisibleFor.Tick(Time.deltaTime);
            if(timer_invisibleFor.IsFinished())
            {
                targetAlpha = 1;
                timer_invisibleFor.Reset();
                timer_invisibleEvery.Reset();
            }
        }

        //apply visibility
        HandleVisibilityChange();
        

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
        int scaleX = ((moveDir * moveSpeed).x >= 0) ? 1 : -1 ;
        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }


    private void HandleVisibilityChange()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        float newAlpha = Mathf.Lerp(renderer.color.a, targetAlpha, invisiblityReduceSpeed * Time.deltaTime);
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, newAlpha);
    }



}
