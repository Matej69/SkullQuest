using UnityEngine;
using System.Collections;


public class Pumpkin : AEnemy {
    
    public GameObject pref_explosion;


    public override void OnStart()
    {
        canBeRotated = false;
    }

    override public void HandleAttack()
    {
        if (!isDying)
        {
            //Explode
            if (Random.Range(0, 2) == 0)
            {
                Explode();
                //as soon as we are in range pumpkin will start dying and become invisible
                health = 0;
                SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);
            }
            //Give coins
            else
            {
                SpawnCoins();
                health = 0;
            }            
        }
    }


    private void Explode()
    {
        Instantiate(pref_explosion, transform.position, Quaternion.identity);
        CharacterStateController.refrence.ReduceHealth(attackDamage);
    }

    private void SpawnCoins()
    {
        
    }





}
