using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [System.NonSerialized] public float HP;

    [System.NonSerialized] public string[] items;

    [System.NonSerialized] public bool openedChest;

    [System.NonSerialized] public float stamina;

    public Slider healthSlider;

    public Slider staminaSlider;


    // Start is called before the first frame update
    void Start()
    {
        HP = GlobalControl.Instance.HP;

        stamina = GlobalControl.Instance.stamina;

        items = GlobalControl.Instance.items;

        openedChest = GlobalControl.Instance.openedChest;

        //dopeInteracted = GlobalControl.Instance.dopeInteracted;

        //schnapInteracted = GlobalControl.Instance.schnapInteracted;

        //if (GameObject.Find("Timeline") && dopeInteracted) { Destroy(GameObject.Find("Timeline")); }
        //else { GameObject.Find("Timeline").SetActive(true); }
        
    }

    public void SavePlayer()
    {
        GlobalControl.Instance.stamina = stamina;

        GlobalControl.Instance.HP = HP;

        GlobalControl.Instance.items = items;

        GlobalControl.Instance.openedChest = openedChest;

    }

}
