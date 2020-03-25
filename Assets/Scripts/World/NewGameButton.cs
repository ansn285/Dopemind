using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class NewGameButton : MonoBehaviour
{
    
    public void ButtonPress()
    {
        GameObject.Find("StartGameTransition").GetComponent<PlayableDirector>().Play();

        Invoke("LoadScene", 2.2f);
    }


    private void LoadScene()
    {
        UnitySceneManager.LoadScene("Scene1");


    }

}
