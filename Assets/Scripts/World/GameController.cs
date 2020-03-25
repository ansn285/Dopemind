using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class GameController : MonoBehaviour
{
    public PlayableDirector schnapServingFood;
    public PlayableDirector dopermineDying;
    public PlayableDirector happyEnding;
    public PlayableDirector genradDeath;

    public GameObject activateTransition;
    
    public GameObject dopermineHealth;

    private GameObject globalObject;


    // Start is called before the first frame update
    void Start()
    {
        globalObject = GameObject.Find("GlobalObject");
        if (UnitySceneManager.GetActiveScene().name == "SchnapHouse" && GameObject.Find("GlobalObject").GetComponent<GlobalControl>().items.Length == 0)
        {
            Invoke("PlaySchnap", 1);
            


        }

        

        if (GameObject.Find("GlobalObject").GetComponent<GlobalControl>().killedSchnap)
        {
            Destroy(GameObject.Find("Audio"));
        }

        if (UnitySceneManager.GetActiveScene().name == "SchnapHouse" && GameObject.Find("GlobalObject").GetComponent<GlobalControl>().killedSchnap)
        {
            GameObject.Find("Switch").SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (UnitySceneManager.GetActiveScene().name == "End")
        {
            if (dopermineHealth.GetComponent<Damage>().health <= 0)
            {
                Destroy(GameObject.Find("Spear")); Destroy(GameObject.Find("MagicBall"));

                gameObject.GetComponent<PauseButton>().enabled = false;

                GameObject.Find("Dopermine").GetComponent<Attack>().CancelInvoke("AfterEverySecond");
                GameObject.Find("Genrad").GetComponent<PlayerStats>().HP = 100;
                GameObject.Find("Genrad").GetComponent<PlayerStats>().stamina = 100;
                GameObject.Find("Genrad").GetComponent<PlayerStats>().items = new string[0];

                globalObject.GetComponent<GlobalControl>().schnapInteracted = false;
                globalObject.GetComponent<GlobalControl>().dopeInteracted = false;
                globalObject.GetComponent<GlobalControl>().killedSchnap = false;
                GameObject.Find("Genrad").GetComponent<PlayerStats>().openedChest = false;

                GameObject.Find("DisplayInformation").GetComponent<Canvas>().enabled = false;

                dopermineDying.Play();

            }

            if (globalObject.GetComponent<GlobalControl>().HP <= 0)
            {
                Destroy(GameObject.Find("Spear")); Destroy(GameObject.Find("MagicBall"));

                gameObject.GetComponent<PauseButton>().enabled = false;
                GameObject.Find("Dopermine").GetComponent<Attack>().CancelInvoke("AfterEverySecond");
                GameObject.Find("Genrad").GetComponent<PlayerStats>().HP = 100;
                GameObject.Find("Genrad").GetComponent<PlayerStats>().stamina = 100;
                GameObject.Find("Genrad").GetComponent<PlayerStats>().items = new string[0];

                globalObject.GetComponent<GlobalControl>().schnapInteracted = false;
                globalObject.GetComponent<GlobalControl>().dopeInteracted = false;
                globalObject.GetComponent<GlobalControl>().killedSchnap = false;
                GameObject.Find("Genrad").GetComponent<PlayerStats>().openedChest = false;

                GameObject.Find("DisplayInformation").GetComponent<Canvas>().enabled = false;

                genradDeath.Play();
            }
        }
        if (UnitySceneManager.GetActiveScene().name == "End")
        {
            if (dopermineDying.state.ToString() == "Playing" ^
                genradDeath.state.ToString() == "Playing" ^
                happyEnding.state.ToString() == "Playing")
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    GameObject.Find("GameController").GetComponent<SceneController>().LoadScene("Menu");
                }
            }
        }


    }

    private void PlaySchnap()
    {

        schnapServingFood.Play();
    }

    public void PlayHappyEnding()
    {
        gameObject.GetComponent<PauseButton>().enabled = false;
        GameObject.Find("Dopermine").GetComponent<FinalState>().enabled = false;

        GameObject.Find("Genrad").GetComponent<PlayerStats>().HP = 100;
        GameObject.Find("Genrad").GetComponent<PlayerStats>().stamina = 100;
        GameObject.Find("Genrad").GetComponent<PlayerStats>().items = new string[0];

        globalObject.GetComponent<GlobalControl>().schnapInteracted = false;
        globalObject.GetComponent<GlobalControl>().dopeInteracted = false;
        globalObject.GetComponent<GlobalControl>().killedSchnap = false;
        GameObject.Find("Genrad").GetComponent<PlayerStats>().openedChest = false;

        happyEnding.Play();

    }

}
