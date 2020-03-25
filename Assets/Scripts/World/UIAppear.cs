using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIAppear : MonoBehaviour
{
    private Animator animator;
    private Animator messageAnim;
    //Asking user for two images
    [SerializeField] private Image customImage;
    [SerializeField] private Image customImage2;

    //Check if the player is still within the box collider or not
    private bool isColliding;

    private bool messageOpen;


    //When player enters the box collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        //If specifically player enters
        if (collision.gameObject.tag == "Player")
        {
            customImage2.enabled = true;
            customImage.enabled = true;

            messageOpen = false;

            animator = customImage2.gameObject.GetComponent<Animator>();
            messageAnim = customImage.gameObject.GetComponent<Animator>();

            //That means player is colliding technically
            animator.Play("ImageEnter");

            isColliding = true;

        }

    }

    //When player leaves the box collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        //If specifically player leaves
        if (collision.gameObject.tag == "Player")
        {
            
            //That means player is no more colliding technically
            isColliding = false;
            customImage.enabled = false;
            animator.Play("ImageLeave");

        }

    }

    //Updates the game every millisecond
    private void Update()
    {
        //When player presses the E key while being inside the box collider and the message board is not showing
        if (Input.GetKeyDown(KeyCode.E) && isColliding == true && customImage.enabled && !messageOpen)
        {
            //Shows the text board and then disables the text
            customImage.enabled = true;

            animator.Play("ImagePressed");

            StartCoroutine(ExecuteAfterTime(.10f));

            /*messageAnim.Play("MessageOpen");
            customImage2.enabled = false;

            messageOpen = true;*/
        }

        //When player presses the E key while being inside the box collider and the message board is showing
        else if (Input.GetKeyDown(KeyCode.E) && customImage.enabled && isColliding == true && messageOpen)
        {
            //Hides the text board and displays the text again
            messageAnim.Play("MessageClose");
            //customImage.enabled = false;
            customImage2.enabled = true;
            animator.Play("ImageEnter");

            messageOpen = false;
        }
        
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        messageAnim.Play("MessageOpen");
        customImage2.enabled = false;

        messageOpen = true;
    }

}
