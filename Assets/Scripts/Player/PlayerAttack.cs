using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    //private int count = 0;

    private float timeBtwAttack;

    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemy;
    public float attackRange;
    public Slider staminaSlider;

    private int count;

    // Update is called once per frame
    void Update()
    {

        if (timeBtwAttack <= 0)
        {
            //then attack

            if (Input.GetMouseButtonDown(0) && gameObject.GetComponent<PlayerStats>().items.Length != 0 && 
                GameObject.Find("GlobalObject").GetComponent<GlobalControl>().stamina > 0)

            {
                gameObject.GetComponent<PlayerStats>().stamina -= 20;

                staminaSlider.value = gameObject.GetComponent<PlayerStats>().stamina;

                CancelInvoke("AfterSomeTime");
                StopAllCoroutines();

                //making a circle collider that gives damage to enemies if in it
                Collider2D[] enemiesToAttack = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);

                gameObject.GetComponent<Animator>().Play("Attack");

                for (int i = 0; i < enemiesToAttack.Length; i++)
                {
                    enemiesToAttack[i].GetComponent<Damage>().TakeDamage(5);
                }


            }
            else if (GameObject.Find("GlobalObject").GetComponent<GlobalControl>().stamina <= 0)
            {
                GameObject.Find("GlobalObject").GetComponent<GlobalControl>().stamina = 0;

                staminaSlider.value = 0;

            }

            if (Input.GetMouseButtonUp(0))
            {
                InvokeRepeating("AfterSomeTime", 0, 0.2f);


            }

            timeBtwAttack = startTimeBtwAttack;

        }

        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

    }


    private void OnDrawGizmosSelected()
    {
        //Drawing a cirlce to know that it is making a collider

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    void AfterSomeTime()
    {
        count++;

        if (count % 5 == 0 && GameObject.Find("GlobalObject").GetComponent<GlobalControl>().stamina < 100)
        {
            StopAllCoroutines();
            StartCoroutine(RegenerateStamina());
        }

    }


    IEnumerator RegenerateStamina()
    {
        while (GameObject.Find("GlobalObject").GetComponent<GlobalControl>().stamina < 100)
        {
            gameObject.GetComponent<PlayerStats>().stamina += 1;

            staminaSlider.value += 1;

            yield return null;
        }


        if (GameObject.Find("GlobalObject").GetComponent<GlobalControl>().stamina >= 100)
        {
            CancelInvoke("AfterSomeTime");

            yield return null;

        }
    }

}
