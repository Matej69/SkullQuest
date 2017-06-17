using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ShopSlot : MonoBehaviour {

    public CharacterStateController.UpgradeStat.E_ID id;

    private Text txt_upgradeName;
    private Text txt_coins;
    private Image[] imgs_slots;
    private Button btn_slot;
      
    private CharacterStateController.UpgradeStat upgradeInfo; 
    
    	
	void Start () 
	{
        upgradeInfo = CharacterStateController.refrence.stats[id];

        txt_upgradeName = transform.FindChild("Text").GetComponent<Text>();
        txt_coins = transform.FindChild("Price").FindChild("Text").GetComponent<Text>();
        imgs_slots = transform.FindChild("Bar").GetComponentsInChildren<Image>();
        btn_slot = GetComponent<Button>();

        SetButtonListeners();
        InitInfoToGUI();
    }

	void Update () 
	{
    }


    void InitInfoToGUI()
    {        
        txt_upgradeName.text = id.ToString().Replace('_', ' ');
        UpdateInfo();
    }

    void UpdateInfo()
    {
        //update cost text
        txt_coins.text = CoinsGUI.GetSeparatedNumber(upgradeInfo.cost);
        //update bars
        for (int i = 0; i < upgradeInfo.lvl; ++i)
            imgs_slots[i].sprite = ShopGUI.refrence.spr_barFilled;            
    }

    void SetButtonListeners()
    {
        btn_slot.onClick.AddListener(delegate{
            //upgrade if there are enough coins
            CharacterStateController.UpgradeStat statInfo = CharacterStateController.refrence.GetStats(id);
            int curCoins = CharacterStateController.refrence.coins;
            if (curCoins >= statInfo.cost && statInfo.CanBeUpgraded())
            {
                AudioManager.CreateSound(AudioManager.E_SOUND.PURCHASE);
                statInfo.Upgrade();
                CharacterStateController.refrence.coins -= statInfo.cost;
                UpdateInfo();
            }
            else
                AudioManager.CreateSound(AudioManager.E_SOUND.PURCHASE_FAILED);
        });
    }



}
