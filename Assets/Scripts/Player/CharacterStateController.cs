using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CharacterStateController : MonoBehaviour {

    public static CharacterStateController refrence;

    [HideInInspector]
    public bool respawning = false;


    public class UpgradeStat
    {
        public enum E_ID {MAX_HEALTH, ATTACK_DMG, HEALTH_REGEN, CRIT_CHANCE, EVADE_CHANCE, COINS}    
        public int lvl;
        public int maxLvl;

        private int[] upgradeValues;
        private int[] costValues;
        public int value {
            get { return upgradeValues[lvl]; }
            set { }
        }
        public int cost {
            get { return (CanBeUpgraded()) ? costValues[lvl + 1] : 0; ; }
            set { }
        }

        public UpgradeStat(int[] _upgradeValues, int[] _costValues) { lvl = 1; maxLvl = 7; upgradeValues = _upgradeValues; costValues = _costValues; }   
        public bool CanBeUpgraded() { return (lvl < maxLvl); }
        public void Upgrade() { lvl = (lvl < maxLvl) ? lvl+1 : lvl;}
    }
    public Dictionary<UpgradeStat.E_ID, UpgradeStat> stats = new Dictionary<UpgradeStat.E_ID, UpgradeStat>();



    [HideInInspector]
    public int health;
    [HideInInspector]
    public int coins;

    Timer timer_healthRegen;

	void Awake()
	{
        refrence = this;

        timer_healthRegen = new Timer(1f);
        InitStatsDictionary();
        respawning = false;
        health = 20;
        coins = 10000000;
    }
	
	void Start () 
	{
    }

	void Update () 
	{
        HandleHealthRegen();
        if (health <= 0)
            respawning = true;
    }


    public void ReduceHealth(int _amount)
    {
        health -= (health - _amount < 0) ? health : _amount;
    }
    private void IncreaseHealth(int _amount)
    {
        int maxHealth = GetStats(UpgradeStat.E_ID.MAX_HEALTH).value;
        health += (health + _amount < maxHealth) ? _amount : (maxHealth - health);
    }
    public void ResetHealth()
    {
        health = GetStats(UpgradeStat.E_ID.MAX_HEALTH).value;
    }

    public void TryReduceHealthOnDamage(int _dmg)
    {
        int evadeChance = CharacterStateController.refrence.GetStats(CharacterStateController.UpgradeStat.E_ID.EVADE_CHANCE).value;
        if (Random.Range(0, 100) > evadeChance)
            CharacterStateController.refrence.ReduceHealth(_dmg);
    }




    public void InitStatsDictionary()
    {
        int[] cost = new int[] {0, 0, 300, 700, 1500, 2800, 4500, 7000};
        stats[UpgradeStat.E_ID.MAX_HEALTH] =        new UpgradeStat(new int[] { 0,  20, 35, 55, 85, 120,160,210 },  cost    );
        stats[UpgradeStat.E_ID.ATTACK_DMG] =        new UpgradeStat(new int[] { 0,  1,  2,  3,  5,  9,  15, 28  },  cost    );
        stats[UpgradeStat.E_ID.HEALTH_REGEN] =      new UpgradeStat(new int[] { 0,  1,  2,  3,  4,  6,  9,  13  },  cost    );
        stats[UpgradeStat.E_ID.CRIT_CHANCE] =       new UpgradeStat(new int[] { 0,  1,  3,  5,  7,  10,  15,  20},  cost    );
        stats[UpgradeStat.E_ID.EVADE_CHANCE] =      new UpgradeStat(new int[] { 0,  1,  2,  4,  7,  10, 12, 20  },  cost    );
        stats[UpgradeStat.E_ID.COINS] =             new UpgradeStat(new int[] { 0,  1,  2,  3,  4,  5,  7,  10  },  cost    );
    }
    public UpgradeStat GetStats(UpgradeStat.E_ID _id)
    {
        return stats[_id];
    }

    public void HandleHealthRegen()
    {
        timer_healthRegen.Tick(Time.deltaTime);
        if (timer_healthRegen.IsFinished())
        {
            IncreaseHealth(GetStats(UpgradeStat.E_ID.HEALTH_REGEN).value);
            timer_healthRegen.Reset();
        }
    }

   


    




}
