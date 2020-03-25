using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseInteract : MonoBehaviour
{
    [SerializeField] private Image interactionImage;
    [SerializeField] private GameObject house;

    private Animator houseAnimator;

    private Animator interactor;

    private bool inCollider;

    private SceneController scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Player")
        {
            scene = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneController>();

            houseAnimator = house.gameObject.GetComponent<Animator>();

            interactor = interactionImage.gameObject.GetComponent<Animator>();

            inCollider = true;

            interactor.Play("ImageEnter");

            houseAnimator.SetBool("nearHouse", true);
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inCollider = false;

            interactor.Play("ImageLeave");

            houseAnimator.SetBool("nearHouse", false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //To enter the house
        if (inCollider && Input.GetKeyDown(KeyCode.E))
        {
            GameObject.Find("FadeAnimation").GetComponent<Animator>().SetTrigger("FadeIn");

            Invoke("LoadHouse", 0.6f);
            
        }

        
    }

    private void LoadHouse()
    {
        scene.LoadScene("SchnapHouse");


    }

}
