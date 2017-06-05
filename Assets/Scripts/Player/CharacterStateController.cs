using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CharacterStateController : MonoBehaviour {

    public static CharacterStateController refrence;

    public class UpgradeStat
    {
        public enum E_ID {MAX_HEALTH, ATTACK_DMG, HEALTH_RENGEN, CRIT_CHANCE, EVADE_CHANCE, COINS}
        public int value;
        public int lvl;
        public int maxLvl;
        public UpgradeStat(int _val, int _lvl, int _maxLvl) { value = _val; lvl = _lvl; maxLvl = _maxLvl;}
    }
    Dictionary<UpgradeStat.E_ID, UpgradeStat> stats = new Dictionary<UpgradeStat.E_ID, UpgradeStat>();

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
        stats[UpgradeStat.E_ID.MAX_HEALTH] = new UpgradeStat(20, 1, 7);
        stats[UpgradeStat.E_ID.ATTACK_DMG] = new UpgradeStat(1, 1, 7);
        stats[UpgradeStat.E_ID.HEALTH_RENGEN] = new UpgradeStat(1, 1, 7);
        stats[UpgradeStat.E_ID.CRIT_CHANCE] = new UpgradeStat(1, 1, 7);
        stats[UpgradeStat.E_ID.EVADE_CHANCE] = new UpgradeStat(1, 1, 7);
        stats[UpgradeStat.E_ID.COINS] = new UpgradeStat(1, 1, 7);
    }
    public UpgradeStat GetStats(UpgradeStat.E_ID _id)
    {
        return stats[_id];
    }




}
