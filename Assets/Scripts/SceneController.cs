using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneController : MonoBehaviour
{
    public static string currentScene = "";
    public static string prevScene = "";

    private Sprite houseDeathScene;
    private SpriteRenderer houseObject;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        currentScene = UnitySceneManager.GetActiveScene().name;
        if (UnitySceneManager.GetActiveScene().name == "Scene2" && GameObject.Find("GlobalObject").GetComponent<GlobalControl>().killedSchnap)
        {
            GameObject.Find("Schnap").SetActive(false);

        }

        else
        {
            return;
        }

        if (UnitySceneManager.GetActiveScene().name == "SchnapHouse" && GameObject.Find("GlobalObject").GetComponent<GlobalControl>().killedSchnap)
        {
            houseDeathScene = GameObject.Find("Switch").GetComponent<KillTrigger>().houseDeath;
            houseObject = GameObject.Find("Switch").GetComponent<KillTrigger>().house;
            houseObject.sprite = houseDeathScene;
            GameObject.Find("Schnap").SetActive(false);
        }
        else
        {
            return;
        }
    }

    public void LoadScene(string sceneName)
    {
        prevScene = currentScene;
        

        UnitySceneManager.LoadScene(sceneName);
    }




}
