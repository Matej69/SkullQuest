using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class HealthBarGUI : MonoBehaviour {

    public Image img_healthbar;
    public Text text;

	void Awake()
	{
	}
	
	void Start () 
	{	
	}

	void Update () 
	{
        UpdateHealthbar();
        UpdateText();
    }



    void UpdateHealthbar()
    {
        int health = CharacterStateController.refrence.health;
        int maxHealth = CharacterStateController.refrence.GetStats(CharacterStateController.UpgradeStat.E_ID.MAX_HEALTH).value;

        float barWidthScale = (health / (float)maxHealth);
        barWidthScale = (barWidthScale <= 0) ? 0 : barWidthScale;
        img_healthbar.transform.localScale = new Vector3(barWidthScale, 1f);
    }
    void UpdateText()
    {
        int health = CharacterStateController.refrence.health;
        int maxHealth = CharacterStateController.refrence.GetStats(CharacterStateController.UpgradeStat.E_ID.MAX_HEALTH).value;
        text.text = health + "/" + maxHealth;
    }



}
