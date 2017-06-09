using UnityEngine;
using System.Collections;


public class CharacterActionController : MonoBehaviour {

    static public CharacterActionController refrence;

    public GameObject pref_slash;
    private Vector2 lastNotZeroDir;
    
    void Awake()
	{
        refrence = this;
	}
	
	void Start () 
	{
        lastNotZeroDir = new Vector2(1, 1);
    }

	void Update () 
	{
        UpdateSlashDirection();
    }

    void UpdateSlashDirection()
    {
        if (Joystick.refrence.GetDir() != Vector2.zero)
            lastNotZeroDir = Joystick.refrence.GetDir();
    }


    public void OnAttack()
    {
        Vector2 slashSpawnPoint = transform.position;

        float rotationAngle = Global.GetAngle(Vector2.right, lastNotZeroDir);

        GameObject slash = (GameObject)Instantiate(pref_slash, slashSpawnPoint, Quaternion.identity);
        slash.GetComponent<ABullet>().damage = CharacterStateController.refrence.GetStats(CharacterStateController.UpgradeStat.E_ID.ATTACK_DMG).value;
        slash.GetComponent<ABullet>().direction = lastNotZeroDir;
        slash.GetComponent<ABullet>().transform.eulerAngles = new Vector3(0, 0, rotationAngle);

        //slash.GetComponent<ABullet>().transform.rotation = Quaternion.Euler(theRotationIWant);
    }




}
