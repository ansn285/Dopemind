using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakingHits : MonoBehaviour
{

    [System.NonSerialized] public bool hasCollided = false;

    public Slider healthSlider;

    private int count = 1;
    //public Text healthText;

    private void Update()
    {
        //healthText.text = "Health: " + gameObject.GetComponent<PlayerStats>().HP;

        if (hasCollided)
        {
            GameObject.Find("Dopermine").GetComponent<Animator>().SetBool("throw", false);

            hasCollided = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Spear" && count > 0)
        {
            collision.gameObject.GetComponent<Animator>().SetBool("isFollowing", false);

            StopAllCoroutines();
            StartCoroutine(ShowDamage("spear"));            
            
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            collision.gameObject.GetComponent<Transform>().position = new Vector2(-9.029383f, -2.379735f);

            hasCollided = true;
            count--;
        }

        else if (collision.gameObject.tag == "MagicBall" && count > 0)
        {
            collision.gameObject.GetComponent<Animator>().SetBool("isFollowing", false);
            collision.GetComponent<SpriteRenderer>().enabled = false;

            collision.GetComponent<CircleCollider2D>().enabled = false;
            

            StopAllCoroutines();
            StartCoroutine(ShowDamage("magic"));

            

            collision.GetComponent<Transform>().position = new Vector2(-8.63f, -0.8371723f);

            hasCollided = true;
            count--;
        }

        else
        {
            count = 1;
        }
    }

    public bool TakenHits()
    {
        if (hasCollided) { return true; }

        else { return false; }


    }

    IEnumerator ShowDamage(string damageFrom)
    {
        if (damageFrom == "spear")
        {
            gameObject.GetComponent<PlayerStats>().HP -= 30;
            for (int i = 0; i < 30; i++)
            {
                healthSlider.value -= 1;

                yield return null;
            }
                        
        }

        if (damageFrom == "magic")
        {
            gameObject.GetComponent<PlayerStats>().HP -= 25;
            for (int i = 0; i < 25; i++)
            {
                healthSlider.value -= 1;

                yield return null;
            }

        }


    }


}
