using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Interact : MonoBehaviour
{
    [System.NonSerialized] public Queue<string> sentences;


    private Animator animator;
    private Animator dialogueAnim;
    private Animator npc;


    public Text nameText;
    public Text dialogueText;


    [SerializeField] Image interaction;

    public Dialogue dialogue;

    
    bool isColliding;

    
    private Rigidbody2D rgd;

    //To check if the player has interacted with the NPC or not
    [System.NonSerialized] public bool interacted = false;


    private void Start()
    {
        //Creating new queue that will hold each sentence in the dialogues in the future
        sentences = new Queue<string>();

        //Creating animator for E to interact button
        animator = interaction.gameObject.GetComponent<Animator>();

        //Creating animator for NPC if they have an animation else than IDLE to play
        npc = GetComponent<Animator>();


        //To make the dialogue box appear with the animation, creating an animator
        dialogueAnim = GameObject.Find("/DialogueCanvas/DialogueBackground").gameObject.GetComponent<Animator>();

        //To prevent the player from moving while interacting with the NPC
        rgd = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Rigidbody2D>();

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When player enters the trigger zone of the NPC
        if (collision.gameObject.tag == "Player")
        {
            //Enables the E to interact button
            interaction.enabled = true;

            //It is check whether the player is still in the trigger zone or not
            isColliding = true;

            //Plays the E to interact image entering animation
            animator.Play("ImageEnter");

            
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //When player leaves the trigger zone
        if (collision.gameObject.tag == "Player")
        {
            //Plays the E to interact image leaving animation
            animator.Play("ImageLeave");

            //When outside the trigger zone, false
            isColliding = false;

        }
    }

    private void Update()
    {
        //When E button is pressed while player is in trigger zone and canvas holding the dialogue box and its belongings is disabled
        if (Input.GetKeyDown(KeyCode.E)  && isColliding && !GameObject.Find("DialogueCanvas").gameObject.GetComponent<Canvas>().enabled)
        {
            if (gameObject.name == "Dopermine")
            {
                //Becomes true when player interacts with the Dopermine
                interacted = true;

                if (GameObject.Find("Timeline"))
                {
                    Destroy(GameObject.Find("Timeline"));
                }

                GameObject.Find("GlobalObject").GetComponent<GlobalControl>().dopeInteracted = true;
            }

            //Freeze all the movements of the player so that it cannot leave while the NPC is talking, it is quite rude
            rgd.constraints = RigidbodyConstraints2D.FreezeAll;

            //Enables the canvas that holds the dialog box and all its belongings
            GameObject.Find("DialogueCanvas").gameObject.GetComponent<Canvas>().enabled = true;

            //E to interact image's preesed animation plays
            animator.Play("ImagePressed");

            //Now the dialogue box appearing animation plays
            dialogueAnim.Play("DialogueEnter");

            //Dialogues actually start
            StartDialogue(dialogue); 
        }

        //When dialogue is not over and player presses E, continues to next dialogue
        else if (Input.GetKeyDown(KeyCode.E) && isColliding && sentences.Count != 0 && GameObject.Find("DialogueCanvas").gameObject.GetComponent<Canvas>().enabled)
        {

            animator.SetTrigger("Pressed");
            DisplayNextSentence();

        }

        //When all the dialogues are displayed, end the dialogue
        else if (Input.GetKeyDown(KeyCode.E) && GameObject.Find("DialogueCanvas").gameObject.GetComponent<Canvas>().enabled == true && isColliding && sentences.Count == 0)
        {
            animator.Play("ImagePressed");
            dialogueAnim.Play("DialogueExit");

            rgd.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

            StartCoroutine(ExecuteAfterTime(1));

        }


    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) { sentences.Enqueue(sentence); }

        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        
        if (sentences.Count == 0)
        {
            EndDialogue();

            

            return;
        }

        string sentence = sentences.Dequeue();

        dialogueText.text = sentence;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach(char letter in sentence.ToCharArray()) { dialogueText.text += letter; yield return null; }
    }

    void EndDialogue()
    {
        GameObject.Find("DialogueCanvas").gameObject.GetComponent<Canvas>().enabled = false;

        if (gameObject.name == "Dopermine")
        {
            GameObject.Find("GlobalObject").GetComponent<GlobalControl>().dopeInteracted = true;
        }

    }

    IEnumerator ExecuteAfterTime(float time)
    {

        yield return new WaitForSeconds(time);

        GameObject.Find("DialogueCanvas").gameObject.GetComponent<Canvas>().enabled = false;

        if (gameObject.name == "Schnap")
        {
            gameObject.GetComponent<NPC_Interact>().enabled = false;
            GameObject.Find("/Schnap/Canvas2").GetComponent<Canvas>().enabled = false;
        }

        foreach (AnimatorControllerParameter parameter in npc.parameters)
        {

            if (parameter.name == "Walk")
            {
                npc.SetTrigger("Walk");


            }
        }
    }

    public bool HasInteracted()
    {
        if (interacted) { return true; }
        else { return false; }
    }

}
