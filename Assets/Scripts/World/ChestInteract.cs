using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestInteract : MonoBehaviour
{
    [SerializeField] private Image interaction;
    [SerializeField] private GameObject items;

    private Animator interAnim;
    private Animator itemAnim;
    private Animator chestAnim;

    private bool isColliding;

    private GameObject player;

    public Sprite ChestOpened;

    private void Start()
    {
        interAnim = interaction.GetComponent<Animator>();
        itemAnim = items.GetComponent<Animator>();
        chestAnim = GetComponent<Animator>();

        player = GameObject.Find("Genrad");
        if (GameObject.Find("GlobalObject").GetComponent<GlobalControl>().openedChest) { gameObject.GetComponent<SpriteRenderer>().sprite = ChestOpened; }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player" && !GameObject.Find("GlobalObject").GetComponent<GlobalControl>().openedChest)
        {
            interAnim.Play("ImageEnter");
            isColliding = true;

        }

        else if (collision.gameObject.tag == "Player" && GameObject.Find("GlobalObject").GetComponent<GlobalControl>().openedChest)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = ChestOpened;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !GameObject.Find("GlobalObject").GetComponent<GlobalControl>().openedChest) { interAnim.Play("ImageLeave"); isColliding = false; }
        else { isColliding = false; }
    }


    private void Update()
    {
        

        if (isColliding && Input.GetKeyDown(KeyCode.E))
        {
            interAnim.Play("ImagePressed");


            chestAnim.SetTrigger("Opened");

            player.GetComponent<PlayerStats>().items = new string[2];

            player.GetComponent<PlayerStats>().items[0] = "Common Knife";

            player.GetComponent<PlayerStats>().items[1] = "Healing Potion";

            player.GetComponent<PlayerStats>().openedChest = true;
            

            interAnim.Play("ImageLeave");

            itemAnim.Play("ItemAppear");

            GameObject.Find("ParryHelpPopup").GetComponent<Canvas>().enabled = true;

            StartCoroutine(ExecuteAfterTime(0.3f));
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        GameObject.Find("GameController").GetComponent<InventorySystem>().potions = 6;
        interaction.enabled = false;

        GameObject.Find("/ParryHelpPopup/Image").GetComponent<Animator>().Play("ParryHelpIn");
        Invoke("DisableCanvas", 9);
    }

    void DisableCanvas()
    {
        GameObject.Find("ParryHelpPopup").GetComponent<Canvas>().enabled = false;
    }

}