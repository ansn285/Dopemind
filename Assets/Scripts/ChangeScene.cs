using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string toScene;



    private SceneController sceneController;

    // Start is called before the first frame update
    void Start()
    {

        sceneController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneController>();
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Genrad").GetComponent<PlayerStats>().SavePlayer();

            GameObject.Find("FadeAnimation").GetComponent<Animator>().SetTrigger("FadeIn");

            Invoke("LoadNextScene", 0.4f);

        }
    }

    public void LoadNextScene()
    {
        sceneController.LoadScene(toScene);
    }

}
