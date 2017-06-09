using UnityEngine;
using System.Collections;


public class AEnemy : MonoBehaviour {

    public enum E_ID { BAT, CAT, EYE, GHOST, PUMPKIN, SPIDER, SPIKE }

    public bool canBeRotated = false;

    public int maxHealth;
    [HideInInspector]
    public int health;
    [HideInInspector]
    public bool isDying = false;

    bool coinsSpawned = false;
    

    [HideInInspector]
    public Vector2 moveDir;
    [Header("MOVEMENT")]
    public float moveSpeed;
    
    protected Vector2 attackDir;
    [Header("ATTACK")]
    public float attackSpeed;
    public float attackRange;
    public int attackDamage;
    
    public enum E_ACTIVITY
    {
        IDLE,
        ATTACKING
    }    
    [HideInInspector]
    public E_ACTIVITY activity;

    private float deathReduceSpeed = 0.15f;
    private BoxCollider2D boxCollider;
    protected GameObject player;
    
    //Timers
    protected Timer timer_changeMoveDir;
    protected Timer timer_changeAttackDir;
    protected Timer timer_attack;    
    [Header("Timer values")]
    public float sec_changeMoveDir;
    public float sec_changeAttackDir;
    public float sec_attack;
    
    


    virtual public void HandleAttack() { }
    virtual public void HandleMovement() { }
    virtual public void OnStart() { }
    
    void Awake()
	{
        boxCollider = GetComponent<BoxCollider2D>();
    }
	
	void Start () 
	{
        player = CharacterStateController.refrence.gameObject;

        timer_changeMoveDir = new Timer(sec_changeMoveDir);
        timer_changeMoveDir.currentTime = 0;    //moveDir is given on isFinish so this will give direction from start
        timer_changeAttackDir = new Timer(sec_changeAttackDir);
        timer_attack = new Timer(sec_attack);
        
        health = maxHealth;
        OnStart();
    }

	void Update () 
	{
        HandleLifetime();
        HandleActivityChange();
        HandleActivityBehaviour();
        TickTimers();
    }


    private void HandleLifetime()
    {
        isDying = (health <= 0) ? true : false;
        //trigger death animation
        if(isDying)
        {
            //change animation
            GetComponent<Animator>().SetBool("isDying", isDying);
            //spawn coins
            if(!coinsSpawned)
            {
                int coinMultiplier = CharacterStateController.refrence.GetStats(CharacterStateController.UpgradeStat.E_ID.COINS).lvl;
                CoinFactory.SpawnCoins(transform.position, (maxHealth + attackDamage) * coinMultiplier);
                coinsSpawned = true;
            }
            //reduce opacity
            if (ChangeOpacity(deathReduceSpeed) <= 0)            
                Destroy(gameObject);
            
        }
    }
    public float ChangeOpacity(float _speed)
    {
        //If there are no children sprites(GameObjects)
        Color col;
        if (gameObject.transform.childCount == 0)
        {
            col = GetComponent<SpriteRenderer>().color;
            col.a -= _speed * Time.deltaTime;
            col.a = (col.a < 0f) ? 0f : ((col.a > 1f) ? 1f : col.a);
            GetComponent<SpriteRenderer>().color = col;
            return col.a;
        }
        //If there are multiple sprites under one gameobject
        else if (gameObject.transform.childCount > 0)
        {
            //Reduce opacity of parent object
            col = GetComponent<SpriteRenderer>().color;
            col.a -= _speed * Time.deltaTime;
            GetComponent<SpriteRenderer>().color = col;
            //reduce opacity for children
            int counter = 0;
            foreach(Transform childTran in transform)
            {
                GameObject go = childTran.gameObject;
                col = go.GetComponent<SpriteRenderer>().color;
                col.a -= _speed * Time.deltaTime;
                col.a = (col.a < 0f) ? 0f : ((col.a > 1f) ? 1f : col.a);
                go.GetComponent<SpriteRenderer>().color = col;

                //if on last child return new opacity
                counter++;
                if (counter == gameObject.transform.childCount)
                {                    
                    return col.a;
                }
            }
        }
        Debug.LogError("ReduceOpacity() is not executed properly");
        return 777;    
    }



    private void HandleActivityBehaviour()
    {
        if (!isDying)
        {
            if (activity == E_ACTIVITY.IDLE)
                HandleMovement();
            else if (activity == E_ACTIVITY.ATTACKING)
                HandleAttack();
        }
    }

    

    private void HandleActivityChange()
    {
        if (IsPlayerInAttackRange())
            activity = E_ACTIVITY.ATTACKING;
        else
            activity = E_ACTIVITY.IDLE;
    }
    public bool IsPlayerInAttackRange()
    {
        if (Vector2.Distance(gameObject.transform.position, player.transform.position) < attackRange)
            return true;
        else
            return false;   
    }


    protected bool IsTouchingPlayer()
    {
        if (boxCollider.bounds.Contains(player.transform.position))
            return true;
        else
            return false;
    }

    private void TickTimers()
    {
        timer_changeMoveDir.Tick(Time.deltaTime);
        timer_attack.Tick(Time.deltaTime);
        timer_changeAttackDir.Tick(Time.deltaTime);
    }
        








}
