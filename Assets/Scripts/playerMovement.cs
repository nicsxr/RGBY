using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    
    public float moveSpeed;
    public float jumpForce;
    private bool isGrounded;
    private bool isCrouched;
    private RigidbodyConstraints2D rc;
    private float originalMoveSpeed;

    public string axis;
    public KeyCode jumpBut;
    public KeyCode crouchBut;

    public bool facingRight = true;

    private Animator anim;
    private SpriteRenderer sr;
    private float moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rc = rb.constraints;
    }
    void Start()
    {
        if(gameObject.tag == "Player 2")
        {
            flip();
        }
        originalMoveSpeed = moveSpeed;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        isCrouched = false;
    }   

    void Update()
    {
        if (Input.GetKeyDown(crouchBut) && isCrouched == false)
        {
            anim.SetBool("isCrouched", true);
            isCrouched = true;
            //rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            moveSpeed = moveSpeed / 2;
        }
        if (Input.GetKeyUp(crouchBut) && isCrouched == true)
        {
            anim.SetBool("isCrouched", false);
            isCrouched = false;
            moveSpeed = originalMoveSpeed;
        }
        if(isCrouched == false)
        {
            moveSpeed = originalMoveSpeed;
        }

        if(moveInput > 0 && facingRight == false)
        {
            flip();
        }
        else if(moveInput < 0 && facingRight == true)
        {
            flip();
        }

        if(moveInput != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    
    private void FixedUpdate()
    {

        moveInput = Input.GetAxisRaw(axis) * moveSpeed;
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(jumpBut) && isGrounded == true)
        {
            rb.velocity = new Vector2(0, jumpForce);
            anim.SetBool("isCrouched", false);
            isCrouched = false;
        }
    }
   

   void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
}
