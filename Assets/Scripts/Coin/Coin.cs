using UnityEngine;
using System.Collections;


public class Coin : MonoBehaviour {
    
    [HideInInspector]
    public int value;
    [HideInInspector]
    public Vector2 targetPos;
    
    private float moveSpeed = 2f;

    Timer timer_startInvincibility;
    Timer timer_lifetime;

    void Start()
    {
        timer_startInvincibility = new Timer(0.55f);
        timer_lifetime = new Timer(16f);
    }

    void Update()
    {
        timer_startInvincibility.Tick(Time.deltaTime);
        HandleTravel();
        HandleLifetime();        
    }

    void OnTriggerEnter2D(Collider2D _col)
    {
        if(_col.CompareTag("PLAYER") && timer_startInvincibility.IsFinished())
        {            
            CharacterStateController.refrence.GetStats(CharacterStateController.UpgradeStat.E_ID.COINS).value += value;
            Destroy(gameObject);
        }
    }




    void HandleTravel()
    {
        Vector2 Pos2D = Vector2.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        transform.position = new Vector3(Pos2D.x, Pos2D.y, transform.position.z);
    }



    void HandleLifetime()
    {
        timer_lifetime.Tick(Time.deltaTime);
        if(timer_lifetime.IsFinished())
        {
            if (ReduceOpacity() <= 0)
                Destroy(gameObject);
        }
    }

    private float ReduceOpacity()
    {
        Color col = GetComponent<SpriteRenderer>().color;
        col.a -= 1f * Time.deltaTime;
        GetComponent<SpriteRenderer>().color = col;
        return col.a;
    }



    public void SetProperties(int _val, Vector2 _targetPos)
    {
        value = _val;
        targetPos = _targetPos;
    }





}
