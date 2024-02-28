using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float iceMoveSpeed;
    public float jumpForce;
    public float jumpMoveSpeed;
    public KeyCode jumpKey, leftKey, rightKey;
    private Transform groundCheck;
    public LayerMask groundLayer, playerLayer;
    float xInput;
    public bool isGrounded;
    public Vector2 GroundCheckSize = new(0, 0);
    // private Vector2 boxSize = new(1.5f, 1f); //***
    private Animator anim;

    public GameObject cannotRide;
    public GameObject Riding = null;
    public bool isOnIce = false;

    [SerializeField] PlayerInput playInput;
    Rigidbody2D rb;

    private void OnValidate()
    {
        playInput = GetComponent<PlayerInput>();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundCheck = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(jumpKey) && isGrounded)
            Jump();

        //       if (Input.GetKeyDown(KeyCode.E))
        //           CheckInteraction();
    }

    private void FixedUpdate()
    {
        xInput = playInput.actions["Move"].ReadValue<Vector2>().x;

        GroundCheck();
        Playermove();
        FlipPlayer();
    }

    void Playermove()
    {
        if (isGrounded)
        {
            if (Riding != null)
            {
                rb.velocity = new Vector2(Riding.GetComponent<Rigidbody2D>().velocity.x, 0) + new Vector2(moveSpeed * xInput, rb.velocity.y);
                anim.SetFloat("Speed", 0f);

            }


            else if (isOnIce)
            {
                rb.AddForce(new Vector2(iceMoveSpeed * xInput, rb.velocity.y));
                anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

            }
            else
            {
                rb.velocity = new Vector2(moveSpeed * xInput, rb.velocity.y);
                anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

            }

        }
        else
        {
            float velX;
            if (rb.velocity.x < 0)
                velX = Mathf.Max(rb.velocity.x + (jumpMoveSpeed * xInput), -(moveSpeed + 1.5f));
            else
                velX = Mathf.Min(rb.velocity.x + (jumpMoveSpeed * xInput), (moveSpeed + 1.5f));

            rb.velocity = new Vector2(velX, rb.velocity.y);
        }
    }

    private void Jump()
    {
        anim.SetBool("IsJumping", true);

        if (Riding != null)
            rb.velocity = new Vector2(Riding.GetComponent<Rigidbody2D>().velocity.x, jumpForce);
        else
            rb.velocity = Vector2.up * jumpForce;
    }

    void GroundCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapBox(groundCheck.position, GroundCheckSize, 0f, groundLayer) || Physics2D.OverlapBox(groundCheck.position, GroundCheckSize, 0f, playerLayer);

        if ((wasGrounded == false) && (isGrounded == true))
        {
            anim.SetBool("IsJumping", false);


        }
    }

    void FlipPlayer()
    {
        if ((rb.velocity.x < -0.1f) && xInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // As long as scale is 1
        }
        else if ((rb.velocity.x > 0.1f) && xInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);

        }
    }

    //the stuff down here are if we wanna add an interaction system (leftovers from copying my other games code)

    //   private void CheckInteraction()
    //   {
    //      RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);
    //
    //       if (hits.Length > 0)
    //       {
    //          foreach(RaycastHit2D rc in hits)
    //           {
    //               if (rc.IsInteractable())
    //               {
    //                   rc.Interact();
    //                   return;
    //               }
    //           }
    //       }
    //   }
}


