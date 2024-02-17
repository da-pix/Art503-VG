using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public KeyCode jumpKey,leftKey,rightKey;
    public Transform groundCheck;
    public LayerMask groundLayer,playerLayer;
    float xInput;
    bool isGrounded;
    // private Vector2 boxSize = new(1.5f, 1f);
    // public Animator animator;


    [SerializeField] PlayerInput playInput;
    Rigidbody2D rb;

    private void OnValidate()
    {
        playInput = GetComponent<PlayerInput>();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            if (isGrounded)
           Jump();
        }

 //       if (Input.GetKeyDown(KeyCode.E))
 //           CheckInteraction();
    }

    private void FixedUpdate()
    {
        xInput = playInput.actions["Move"].ReadValue<Vector2>().x;

        Playermove();
        FlipPlayer();
        GroundCheck();
    }

    void Playermove()
    {
        rb.velocity = new Vector2(moveSpeed * xInput, rb.velocity.y);
     //       animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
     //       animatorLeg.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    private void Jump()
    {
     //   animator.SetBool("IsJumping", true);
     //   animatorLeg.SetBool("IsJumping", true);
        rb.velocity = Vector2.up * jumpForce;
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer) || Physics2D.OverlapCircle(groundCheck.position, 0.2f, playerLayer);
        if (isGrounded)
        {
         //   animator.SetBool("IsJumping", false);
         //   animatorLeg.SetBool("IsJumping", false);

        }
    }

    void FlipPlayer()
    {
        if (rb.velocity.x < -0.1f)
        {
            transform.localScale = new Vector3( -1, 1, 1); // As long as scale is 1
        }
        else if (rb.velocity.x > 0.1f)
        {
            transform.localScale = new Vector3( 1, 1, 1);

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


