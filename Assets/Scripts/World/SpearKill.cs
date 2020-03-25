using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SpearKill : MonoBehaviour
{
    private NPC_Interact interacted;

    private GameObject npc;

    Rigidbody2D player;

    public PlayableDirector playableDirector;

    public Canvas cinematicBackground;

    private void Start()
    {
        
        npc = GameObject.Find("Dopermine");

        interacted = npc.gameObject.GetComponent<NPC_Interact>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();


    }

    private void Update()
    {
        if (GameObject.Find("Timeline") && playableDirector.state.ToString() == "Playing")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameObject.Find("GameController").GetComponent<SceneController>().LoadScene("Menu");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!GameObject.Find("GlobalObject").GetComponent<GlobalControl>().dopeInteracted)
            {
                GameObject.Find("GameController").GetComponent<PauseButton>().enabled = false;

                player.constraints = RigidbodyConstraints2D.FreezeAll;

                cinematicBackground.enabled = true;

                cinematicBackground.GetComponent<Animator>().Play("BordersEntering");

                StartCoroutine(ExecuteAfter(.25f));

            }
        }
    }

    IEnumerator ExecuteAfter(float time)
    {
        yield return new WaitForSeconds(time);

        playableDirector.Play();

    }

}
