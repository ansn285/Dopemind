using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalState : MonoBehaviour
{
    
    [SerializeField] Text nameContainer;

    [SerializeField] Text dialogueContainer;

    [SerializeField] Image interactionImage;

    private Animator interactionAnim;

    private Animator dialogueAnim;

    private Animator dope;

    private GameObject player;

    [System.NonSerialized] public bool isHostile;

    [System.NonSerialized] public bool isEnemy = false;

    private bool isColliding = false;

    private Rigidbody2D rb;

    private Queue<string> sentences;

    public Dialogue dialogue;

    [System.NonSerialized] public NPC_Interact npcScript;

    private int count;

    private void Start()
    {
        count = 1;
        sentences = new Queue<string>();


        player = GameObject.Find("Genrad");

        rb = player.GetComponent<Rigidbody2D>();

        dialogue.name = "D O P E R M I N E";
        

        //Checking if player has items in his inventory then he's hostile
        if (player.gameObject.GetComponent<PlayerStats>().items.Length != 0)
        {
            isHostile = true;

            dialogue.sentences = new string[5];

            dialogue.sentences[0] = "H e y    w e    m e e t    a g a i n    G e n r a d . . .";
            dialogue.sentences[1] = "T h a t ' s    a    c o o l    k n i f e    y o u    g o t    t h e r e . . .";
            dialogue.sentences[2] = "M a y b e    y o u    k i l l e d    s o m e o n e    o n    y o u r    w a y . . .";
            dialogue.sentences[3] = "M a y b e    t h a t    i s n ' t    t h e    c a s e . . .";
            dialogue.sentences[4] = "E i t h e r    w a y    I    c a n n o t    l e t    y o u    g o    a n y    f u r t h e r .     \nA    t h r e a t    l i k e    y o u    m u s t    b e    e l i m i n a t e d .";
        }

        //If he doesn't have any item then he's not hostile
        else
        {
            isHostile = false;

            dialogue.sentences = new string[5];
            dialogue.sentences[0] = "W e    m e e t    a g a i n    G e n r a d . . .";
            dialogue.sentences[1] = "I ' m    h a p p y    t o    s e e    t h a t    y o u ' r e    q u i t e    a    \nf r i e n d l y    i n d i v i d u a l . . .";
            dialogue.sentences[2] = "A s    a    t o k e n    o f    m y    t r u s t ,    I    w o u l d    l i k e    t o    \ng i v e    t h i s    f r u i t    t o    y o u. . .";
            dialogue.sentences[3] = "D o    n o t    t h i n k    o f    t h i s    f r u i t    a s    a    c o m m o n    \nf r u i t    a s    i t    i s    t h e    m o s t    r a r e    a n d    t h e    \nt a s t i e s t    f r u i t    i n    t h e    w h o l e    f o r e s t . . .";
            dialogue.sentences[4] = "I    g i v e    y o u    t h i s ,    p l e a s e    a c c e p t    t h i s    a s    a    \ng i f t .";
        }


        interactionAnim = interactionImage.gameObject.GetComponent<Animator>();

        dialogueAnim = GameObject.Find("/DialogueCanvas/DialogueBackground").gameObject.GetComponent<Animator>();

        dope = gameObject.GetComponent<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactionImage.enabled = true;

            interactionAnim.Play("ImageEnter");

            isColliding = true;
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactionAnim.Play("ImageLeave");

            isColliding = false;
        }
    }

    private void Update()
    {


        if (isColliding && Input.GetKeyDown(KeyCode.E) && !GameObject.Find("DialogueCanvas").gameObject.GetComponent<Canvas>().enabled)
        {
            if (player.transform.position.x >= -8.6f)
            {
                player.transform.position = new Vector2(-9.4f, -2.77f);

            }

            GameObject.Find("DialogueCanvas").gameObject.GetComponent<Canvas>().enabled = true;

            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            interactionAnim.Play("ImagePressed");

            dialogueAnim.Play("DialogueEnter");

            StartDialogue(dialogue);

        }

        else if (isColliding && Input.GetKeyDown(KeyCode.E) && sentences.Count != 0 && GameObject.Find("DialogueCanvas").gameObject.GetComponent<Canvas>().enabled)
        {
            interactionAnim.Play("ImagePressed");

            DisplayNextSentence();

        }

        else if (Input.GetKeyDown(KeyCode.E) && GameObject.Find("DialogueCanvas").gameObject.GetComponent<Canvas>().enabled == true && isColliding && sentences.Count == 0)
        {
            interactionAnim.Play("ImagePressed");
            dialogueAnim.Play("DialogueExit");

            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            StartCoroutine(ExecuteAfter(0.5f));

        }

    }


    private void StartDialogue(Dialogue dialogue)
    {
        nameContainer.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    private void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
        }

        string sentence = sentences.Dequeue();

        

        StopAllCoroutines();

        StartCoroutine(ShowCharacters(sentence));

    }


    private void EndDialogue()
    {
        GameObject.Find("DialogueCanvas").gameObject.GetComponent<Canvas>().enabled = false;

    }


    IEnumerator ShowCharacters(string sentence)
    {
        dialogueContainer.text = "";

        foreach (char letter in sentence) { dialogueContainer.text += letter; yield return null; }
    }

    IEnumerator ExecuteAfter(float time)
    {
        yield return new WaitForSeconds(time);

        GameObject.Find("DialogueCanvas").gameObject.GetComponent<Canvas>().enabled = false;

        

        if (isHostile)
        {
            if (count > 0)
            {
                if (GameObject.Find("Audio"))
                {
                    GameObject.Find("Audio").GetComponent<AudioSource>().Stop();
                }
                GameObject.Find("CombatMusic").GetComponent<AudioSource>().Play();

                count--;
            }
            

            gameObject.GetComponent<Animator>().SetTrigger("isEvil");

            isEnemy = true;
            

            GameObject.Find("GameObject (2)").GetComponent<BoxCollider2D>().enabled = false;

            GameObject.Find("GameObject (5)").GetComponent<BoxCollider2D>().enabled = true;

            GameObject.Find("/Dopermine/Canvas2").GetComponent<Canvas>().enabled = false;

            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            GameObject.Find("/Dopermine/DoperHitBox").GetComponent<BoxCollider2D>().enabled = true;

            GameObject.Find("DisplayInformation").GetComponent<Canvas>().enabled = true;

            Invoke("StartAttackTimer", 2);
        }

        else if (!isHostile)
        {
            GameObject.Find("GameController").GetComponent<GameController>().PlayHappyEnding();

        }

    }

    private void StartAttackTimer()
    {
        gameObject.GetComponent<Attack>().InvokeRepeating("AfterEverySecond", 0, 1);


    }


}
