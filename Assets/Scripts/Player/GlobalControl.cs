using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;
using UnityEngine.UI;

public class GlobalControl : MonoBehaviour
{
    public float HP;

    public float stamina;


    public string[] items;


    public static GlobalControl Instance;

    public bool openedChest;

    public bool dopeInteracted = false;
    public bool schnapInteracted = false;
    public bool killedSchnap = false;

    public float volume;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);

            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        if (!dopeInteracted && GameObject.Find("Dopermine") && UnitySceneManager.GetActiveScene().name == "Scene1")
        {
            GameObject.Find("SpearKillTrigger").GetComponent<SpearKill>().enabled = true;

        }
        else if (dopeInteracted && GameObject.Find("Dopermine") && UnitySceneManager.GetActiveScene().name == "Scene1")
        {
            GameObject.Find("SpearKillTrigger").GetComponent<SpearKill>().enabled = false;
        }

        /*if (schnapInteracted && UnitySceneManager.GetActiveScene().name == "Scene2")
        {
            GameObject.Find("Schnap").SetActive(false);
        }
        else if (killedSchnap && GameObject.Find("Schnap") && UnitySceneManager.GetActiveScene().name == "Scene2")
        {
            GameObject.Find("Schnap").SetActive(false);
        }
        else if (killedSchnap && GameObject.Find("Schnap") && UnitySceneManager.GetActiveScene().name == "SchnapHouse")
        {
            GameObject.Find("Schnap").SetActive(false);

            house.sprite = houseDeathScene;

        }*/

    }


}
