using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{

    private bool isOpen = false;

    public Canvas canvas;

    public Text emptyInventoryText;
    public GameObject allItemsContainer;
    public Text potionQuantityText;
    public Text potionsText;

    [System.NonSerialized] public int potions;

    private void Start()
    {
        if (GameObject.Find("GlobalObject").GetComponent<GlobalControl>().items.Length != 0)
        {
            allItemsContainer.SetActive(true);
            emptyInventoryText.gameObject.SetActive(false);
            potions = 6;
        }
        else
        {
            emptyInventoryText.gameObject.SetActive(true);
            allItemsContainer.SetActive(false);
        }

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            Time.timeScale = 0;
            canvas.enabled = true;

            isOpen = true;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) | Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            Time.timeScale = 1;
            canvas.enabled = false;

            isOpen = false;

        }

        if (GameObject.Find("GlobalObject").GetComponent<GlobalControl>().items.Length != 0)
        {
            allItemsContainer.SetActive(true);
            emptyInventoryText.gameObject.SetActive(false);
        }
        else
        {
            emptyInventoryText.gameObject.SetActive(true);
            allItemsContainer.SetActive(false);
        }
        if (potionsText != null)
        {
            potionsText.text = potions.ToString();
        }
        potionQuantityText.text = "Q U A N T I T Y :    " + potions;
        GameObject.Find("/DisplayInformation/Potion/Text").GetComponent<Text>().text = potions.ToString();


    }

    public void OpenInventory()
    {
        canvas.enabled = true;

        isOpen = true;

    }


    public void UsePotion()
    {
        if (potions > 0)
        {
            canvas.enabled = false;
            Time.timeScale = 1;

            GameObject.Find("Genrad").GetComponent<Animator>().Play("Healing");

            isOpen = false;

            if (GameObject.Find("Genrad").GetComponent<PlayerStats>().HP <= 70)
            {
                GameObject.Find("Genrad").GetComponent<PlayerStats>().HP += 30;

            }

            else
            {
                GameObject.Find("Genrad").GetComponent<PlayerStats>().HP = 100;

            }
            
            /*if (GameObject.Find("GlobalObject").GetComponent<GlobalControl>().HP + 30 < 100)
            {
                GameObject.Find("GlobalObject").GetComponent<GlobalControl>().HP += 30;
            }

            else if (GameObject.Find("GlobalObject").GetComponent<GlobalControl>().HP + 30 >= 100)
            {
                GameObject.Find("GlobalObject").GetComponent<GlobalControl>().HP = 100;
            }*/

            StopAllCoroutines();
            StartCoroutine(IncreaseHealth());

            potions--;

            
        }

    }

    IEnumerator IncreaseHealth()
    {
        for (int i = 30; i > 0; i--)
        {
            if (GameObject.Find("Genrad").GetComponent<PlayerStats>().HP < 100)
            { GameObject.Find("/DisplayInformation/GenradHealthBar").GetComponent<Slider>().value += 1; yield return null; }
            else { GameObject.Find("/DisplayInformation/GenradHealthBar").GetComponent<Slider>().value = 100; yield return null; }


            /*if (GameObject.Find("Genrad").GetComponent<PlayerStats>().HP < 70)
            {
                GameObject.Find("Genrad").GetComponent<PlayerStats>().HP += 30;

                yield return null;
            }

            else
            {
                GameObject.Find("Genrad").GetComponent<PlayerStats>().HP = 100;

                yield return null;
            }*/



        }
        
    }


}
