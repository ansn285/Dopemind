using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator npc;

    public Transform spear;

    public Transform magicBall;

    private Transform playerTransform;

    private bool thrown = false;

    private int num;

    [System.NonSerialized] public int count=0;

    private void Start()
    {
        npc = gameObject.GetComponent<Animator>();

        playerTransform = GameObject.Find("Genrad").GetComponent<Transform>();
    }

    private void PrepareAttack()
    {
        if (gameObject.GetComponent<FinalState>().isEnemy && num < 2 && count == 8)
        {
            npc.Play("SpearThrow");

            thrown = true;

            LaunchAttack("spear");

        }

        if (gameObject.GetComponent<FinalState>().isEnemy && num == 2 && count == 8)
        {
            npc.Play("MagicAttack");

            thrown = true;

            LaunchAttack("magic");

        }


    }

    private void LaunchAttack(string attack)
    {
        if (attack == "spear" && thrown && !GameObject.Find("Genrad").GetComponent<TakingHits>().hasCollided)
        {            
            Invoke("SpearAppear", 2.3f);

            thrown = false;
        }

        else if (attack == "magic" && thrown && !GameObject.Find("Genrad").GetComponent<TakingHits>().hasCollided)
        {
            Invoke("MagicAppear", 1.6f);
            
            thrown = false;
        }



    }




    void AfterEverySecond()
    {
        if (GameObject.Find("/Dopermine/DoperHitBox").gameObject.GetComponent<Damage>().health > 0)
        {
            PrepareAttack();
        }

        if (count == 8 && gameObject.GetComponent<FinalState>().isEnemy) { count = 0; }

        count++;


        if (num != 0 || num != 1)
        {
            num = Random.Range(0, 3);
        }
    }

    void SpearAppear()
    {
        GameObject.Find("Spear").GetComponent<SpriteRenderer>().enabled = true;

        GameObject.Find("Spear").GetComponent<BoxCollider2D>().enabled = true;

    }

    void MagicAppear()
    {

        GameObject.Find("MagicBall").gameObject.GetComponent<SpriteRenderer>().enabled = true;

        GameObject.Find("MagicBall").gameObject.GetComponent<CircleCollider2D>().enabled = true;

    }


}
