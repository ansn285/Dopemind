using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class LoadOnActivation : MonoBehaviour
{

    private void OnEnable()
    {
        GameObject.Find("GameController").GetComponent<SceneController>().LoadScene("Menu");

    }

}
