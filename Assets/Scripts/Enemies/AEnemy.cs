using UnityEngine;
using System.Collections;


public class AEnemy : MonoBehaviour {

    public int health;
    [HideInInspector]
    public bool isDying = false;
   
    protected Vector2 moveDir;
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

    private float alphaReduceSpeed = 0.15f;
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
        timer_changeAttackDir = new Timer(sec_changeAttackDir);
        timer_attack = new Timer(sec_attack);

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
            //reduce opacity
            if (ReduceOpacity() <= 0)
                Destroy(gameObject);
        }
    }
    private float ReduceOpacity()
    {
        //If there are no children sprites(GameObjects)
        if (gameObject.transform.childCount == 0)
        {
            Color col = GetComponent<SpriteRenderer>().color;
            col.a -= alphaReduceSpeed * Time.deltaTime;
            GetComponent<SpriteRenderer>().color = col;
            return col.a;
        }
        //If there are multiple sprites under one gameobject
        else if (gameObject.transform.childCount > 0)
        {
            //Reduce opacity of parent object
            Color col = GetComponent<SpriteRenderer>().color;
            col.a -= alphaReduceSpeed * Time.deltaTime;
            GetComponent<SpriteRenderer>().color = col;
            //reduce opacity for children
            int counter = 0;
            foreach(Transform childTran in transform)
            {
                GameObject go = childTran.gameObject;
                col = go.GetComponent<SpriteRenderer>().color;
                col.a -= alphaReduceSpeed * Time.deltaTime;
                go.GetComponent<SpriteRenderer>().color = col;

                //if on last child return new opacity
                counter++;
                if(counter == gameObject.transform.childCount)
                    return col.a;
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
