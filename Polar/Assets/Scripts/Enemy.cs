using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool shouldJump;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    public float jumpForce;
    private SpriteRenderer sr;
    private bool isAirBorne = false;
    public LayerMask groundLayer;
    private Transform groundCheck;
    private GameObject jumpT;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        groundCheck = transform.GetChild(0);
        jumpT = transform.GetChild(1).gameObject;
    }

    private IEnumerator JumpUp()
    {
        bc.enabled = false;
        rb.AddForce(new Vector2(0f, jumpForce));
        Debug.Log("dawg up");
        while (!isAirBorne)
        {
            if (!Physics2D.OverlapCircle(groundCheck.position, .2f, groundLayer))
            {
                isAirBorne = true;
                bc.enabled = true;

            }
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Destroy(jumpT);
            jumpT.SetActive(false);
            rb.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(JumpUp());
        }
    }
}
