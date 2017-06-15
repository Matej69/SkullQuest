using UnityEngine;
using System.Collections;


public class Spider : AEnemy {

    [Header("Spider specific")]
    public GameObject pref_smallSpider;
    public GameObject pref_web;
    public float spawnSpiderEvery;

    Timer timer_spawnSpider;

    public override void OnStart()
    {
        timer_spawnSpider = new Timer(spawnSpiderEvery);
    }


    override public void HandleAttack()
    {
        RotateTowardsPlayer();

        timer_attack.Tick(Time.deltaTime);
        if (timer_attack.IsFinished())
        {
            if (Random.Range(0, 2) == 1)
                SpawnWeb();
            timer_attack.Reset();
        }
        //spawn spiders
        timer_spawnSpider.Tick(Time.deltaTime);
        if (timer_spawnSpider.IsFinished())
        {
            SpawnSpider();
            timer_spawnSpider.Reset();
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



    private void SpawnWeb()
    {
        GameObject web = (GameObject)Instantiate(pref_web, transform.position, Quaternion.identity);
        web.GetComponent<ABullet>().direction = (player.transform.position - transform.position).normalized; 
    }

    private void SpawnSpider()
    {
        Instantiate(pref_smallSpider, transform.position, Quaternion.identity);
    }


}
