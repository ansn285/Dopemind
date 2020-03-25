using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillTrigger : MonoBehaviour
{
    public Image interactionImage;
    public Sprite houseDeath;
    public SpriteRenderer house;

    private Animator interAnim;
    private bool isColliding = false;

    public AudioSource audioSource;
    public AudioClip suspenseSound;

    private void Start()
    {
        interAnim = interactionImage.gameObject.GetComponent<Animator>();

        if (GameObject.Find("GlobalObject").GetComponent<GlobalControl>().items.Length != 0)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;

            interactionImage.enabled = true;

        }

        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            interactionImage.enabled = false;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interAnim.Play("ImageEnter");

            isColliding = true;
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interAnim.Play("ImageLeave");

            isColliding = false;
        }


    }

    private void Update()
    {
        if (isColliding && Input.GetKeyDown(KeyCode.E) && GameObject.Find("GlobalObject").GetComponent<GlobalControl>().items.Length != 0)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().position = new Vector3(-10, 0, -10);
            Destroy(GameObject.Find("Audio"));
            audioSource.Stop();
            Invoke("DeathScene", 4);
        }


    }


    private void DeathScene()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().ChangeAudioClip(suspenseSound);

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().position = new Vector3(0, 0, -10);

        GameObject.Find("Switch").SetActive(false);

        GameObject.Find("Schnap").GetComponent<SpriteRenderer>().enabled = false;

        house.sprite = houseDeath;

        GameObject.Find("GlobalObject").GetComponent<GlobalControl>().killedSchnap = true;
    }


}
