using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrySystem : MonoBehaviour
{
    /*private bool isColliding = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spear")
        {
            isColliding = true;

        }

    }

    private void Update()
    {


        if (isColliding && Input.GetMouseButtonDown(1)) { Debug.Log("Parry"); isColliding = false; }
    }*/

    public Transform parryPos;

    public float x; public float y;

    public LayerMask whatIsAttack;


    private void Update()
    {
        //making a box collider for parry system

        if (Input.GetMouseButtonDown(1) && gameObject.GetComponent<PlayerStats>().items.Length != 0)
        {
            gameObject.GetComponent<Animator>().Play("Parry");


            Collider2D[] attackToParry = Physics2D.OverlapBoxAll(parryPos.position, new Vector2(x, y), 0, whatIsAttack);

            for (int i = 0; i < attackToParry.Length; i++)
            {
                GameObject.Find("Spear").GetComponent<Animator>().GetBehaviour<SpearMove>().inverse = true;
            }


        }
    }

    private void OnDrawGizmosSelected()
    {
        //Drawing a cirlce to know that it is making a collider

        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(parryPos.position, new Vector3(x, y, 0));
    }


}
