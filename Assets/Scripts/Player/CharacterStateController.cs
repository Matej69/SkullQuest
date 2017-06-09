using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CharacterStateController : MonoBehaviour {

    public static CharacterStateController refrence;

    public class UpgradeStat
    {
        public enum E_ID {MAX_HEALTH, ATTACK_DMG, HEALTH_RENGEN, CRIT_CHANCE, EVADE_CHANCE, COINS}    
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
        public void Upgrade() { lvl++;}
    }
    public Dictionary<UpgradeStat.E_ID, UpgradeStat> stats = new Dictionary<UpgradeStat.E_ID, UpgradeStat>();

    [HideInInspector]
    public int health;
    

	void Awake()
	{
        refrence = this;
        InitStatsDictionary();
        health = 20;
    }
	
	void Start () 
	{	
	}

	void Update () 
	{
    }


    public void ReduceHealth(int _amount)
    {
        health -= (health - _amount < 0) ? health : _amount;
    }



    public void InitStatsDictionary()
    {
        int[] cost = new int[] {0, 0, 300, 700, 1500, 2800, 4500, 7000};
        stats[UpgradeStat.E_ID.MAX_HEALTH] =        new UpgradeStat(new int[] { 0,  20, 35, 55, 85, 120,160,210 },  cost    );
        stats[UpgradeStat.E_ID.ATTACK_DMG] =        new UpgradeStat(new int[] { 0,  1,  2,  3,  5,  9,  15, 28  },  cost    );
        stats[UpgradeStat.E_ID.HEALTH_RENGEN] =     new UpgradeStat(new int[] { 0,  1,  2,  3,  4,  6,  9,  13  },  cost    );
        stats[UpgradeStat.E_ID.CRIT_CHANCE] =       new UpgradeStat(new int[] { 0,  1,  2,  3,  4,  5,  6,  7   },  cost    );
        stats[UpgradeStat.E_ID.EVADE_CHANCE] =      new UpgradeStat(new int[] { 0,  1,  2,  4,  7,  11, 16, 20  },  cost    );
        stats[UpgradeStat.E_ID.COINS] =             new UpgradeStat(new int[] { 0,  1,  2,  3,  4,  5,  7,  10  },  cost    );
    }
    public UpgradeStat GetStats(UpgradeStat.E_ID _id)
    {
        return stats[_id];
    }




}
