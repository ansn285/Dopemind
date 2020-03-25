using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    public Slider healthBar;

    [System.NonSerialized] public int health = 1000;



    public void TakeDamage(int damage)
    {
        //Take damage after every hit
        health -= damage;

        StopAllCoroutines();

        StartCoroutine(ShowDamage(damage));


    }


    IEnumerator ShowDamage(int damage)
    {
        if (health > 0)
        {
            if (damage > 5)
            {
                while (healthBar.value != health)
                {
                    healthBar.value -= 10;

                    yield return null;
                }

            }

            else
            {
                healthBar.value = health;
            }

        }

        else
        {
            health = 0;

            healthBar.value = 0;

            yield return null;
        }

    }


}
