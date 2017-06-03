using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class WeaponGUI : MonoBehaviour {   

    void Start()
    {
        SetOnClickListener();
    }


    void SetOnClickListener()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            CharacterActionController.refrence.OnAttack();
        });
    }





}
