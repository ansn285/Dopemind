using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;

    public float speed = 10f;

    public bool isFacingLeft = false;

    private Rigidbody2D rb;

    private Vector2 moveVelocity;
    private int potions = 6;

    private string buttonPressed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        moveVelocity = moveInput * speed;

        animator.SetFloat("Walking", Mathf.Abs(moveInput[0]));

        if (Input.GetAxis("Horizontal") < 0.0f && isFacingLeft == false) { FlipCharacter(); }
        else if (Input.GetAxis("Horizontal") > 0.0f && isFacingLeft == true) { FlipCharacter(); }

        if (Input.GetKeyDown(KeyCode.F) && potions > 0)
        {
            GameObject.Find("GameController").GetComponent<InventorySystem>().UsePotion();            
        }

        GameObject.Find("Genrad").GetComponent<PlayerStats>().SavePlayer();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);
    }

    private void FlipCharacter()
    {
        isFacingLeft = !isFacingLeft;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
