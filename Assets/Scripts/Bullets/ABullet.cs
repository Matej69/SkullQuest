using UnityEngine;
using System.Collections;


abstract public class ABullet : MonoBehaviour {
        
    private Timer timer_lifetime;
    public float lifeTime;
    private float alphaReduceSpeed = 6f;
    private bool isLifeEndEffectTriggered = false;
    private bool canHit = true;    

    public enum E_BULLET_OWNER
    {
        PLAYER,
        ENEMY
    };

    [HideInInspector]
    public Vector2 direction;
    public float speed;
    public E_BULLET_OWNER ownerID;
    public int damage;
    public int hitCounter;


    virtual public void OnStart() { }
    virtual public void OnLifeEndEffect() { }             //Spawn spiders, spawn toxic,.....
    virtual public void OnHitEnemy(GameObject _enemy)
    {
        _enemy.GetComponent<AEnemy>().health -= damage;        
    }
    virtual public void OnHitPlayer()
    {
        CharacterStateController.refrence.ReduceHealth(damage);
    }           



    void Start () 
	{
        timer_lifetime = new Timer(lifeTime);
        OnStart();
    }

	void Update ()
    {      
        HandleLifetime();
        HandleMovement();
    }





    void OnTriggerEnter2D(Collider2D _col)
    {
        if (canHit)
        {
            if (ownerID == E_BULLET_OWNER.PLAYER && _col.gameObject.CompareTag("ENEMY"))
            {
                OnHitEnemy(_col.gameObject);
                hitCounter--;
            }
            else if (ownerID == E_BULLET_OWNER.ENEMY && _col.gameObject.CompareTag("PLAYER"))
            {
                OnHitPlayer();
                hitCounter--;
            }
        }

        canHit = (hitCounter > 0) ? true : false;
        //Slash
        //Spider web
        //Cacuna
        //Spikes
    }
    



    //On lifetime end reduce opacity and destroy GO
    void HandleLifetime()
    {          
        timer_lifetime.Tick(Time.deltaTime);            
        if(timer_lifetime.IsFinished())
        {
            //Called once before destruction
            if (!isLifeEndEffectTriggered)
            {
                OnLifeEndEffect();
                isLifeEndEffectTriggered = true;
            }
            //Called every frame until destruction
            float alphaColor = ReduceOpacity();
            if (alphaColor <= 0)
                Destroy(gameObject);
        }         
    }
    private float ReduceOpacity()
    {
        Color col = GetComponent<SpriteRenderer>().color;
        col.a -= alphaReduceSpeed * Time.deltaTime;
        GetComponent<SpriteRenderer>().color = col;
        return col.a;
    }


    void HandleMovement()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);        
    }



}
