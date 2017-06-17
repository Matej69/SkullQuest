using UnityEngine;
using System.Collections;


public class SpikeBall : AEnemy {

    [Header("Spider specific")]
    public GameObject pref_spike;

    public override void OnDeath()
    {
        base.OnDeath();
        GameManager.refrence.IncreaseDeadBossessCount();        
    }


    override public void HandleAttack()
    {
        RotateTowardsPlayer();

        timer_attack.Tick(Time.deltaTime);
        if (timer_attack.IsFinished())
        {
            if (Random.Range(0, 3) == 1)
                SpawnSpike();
            timer_attack.Reset();
        }
    }

    override public void HandleMovement()
    {
        RotateTowardsPlayer();
        health = maxHealth;
    }

    private void RotateTowardsPlayer()
    {
        if (Vector2.Distance(transform.position, player.transform.position) > 0.05f)
        {
            //change direction
            attackDir = (player.transform.position - transform.position).normalized;
            moveDir = attackDir;
            float rotationAngle = Global.GetAngle(Vector2.right, attackDir) + 90;
            transform.eulerAngles = new Vector3(0, 0, rotationAngle);
        }
    }



    private void SpawnSpike()
    {
        GameObject spike = (GameObject)Instantiate(pref_spike, transform.position, Quaternion.identity);
        spike.GetComponent<ABullet>().direction = (player.transform.position - transform.position).normalized;
        //change direction
        Vector2 dir = (player.transform.position - transform.position).normalized;
        float rotationAngle = Global.GetAngle(Vector2.right, dir) + 90;
        spike.transform.eulerAngles = new Vector3(0, 0, rotationAngle);
    }

}
