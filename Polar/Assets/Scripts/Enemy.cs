using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public Camera cam;
    public float distanceCamJoin;
    private bool joinedCam = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        groundCheck = transform.GetChild(0);
        jumpT = transform.GetChild(1).gameObject;

        if (shouldJump)
        {
            transform.position = transform.position - new Vector3(0, 2.5f, 0);
        }
    }

    private void Update()
    {

        //Debug.Log(Vector3.Distance(transform.position, cam.GetComponent<MultipletargetCamera>().getCentrePointOfPlayers()));

        if (Vector3.Distance(transform.position, cam.GetComponent<MultipletargetCamera>().getCentrePointOfPlayers()) < distanceCamJoin && !joinedCam)
        {
            cam.GetComponent<MultipletargetCamera>().targets.Add(transform);
            joinedCam = true;
        }
        else if(Vector3.Distance(transform.position, cam.GetComponent<MultipletargetCamera>().getCentrePointOfPlayers()) > distanceCamJoin && joinedCam)
        {
            cam.GetComponent<MultipletargetCamera>().targets.Remove(transform);
            joinedCam = false;
        }
    }

    public IEnumerator JumpUp()
    {
        bc.enabled = false;
        rb.AddForce(new Vector2(0f, jumpForce));
        //Debug.Log("dawg up");
        while (!isAirBorne)
        {
            if (!Physics2D.OverlapCircle(groundCheck.position, .2f, groundLayer))
            {
                isAirBorne = true;
                bc.enabled = true;
                yield return new WaitForSeconds(.7f);
                sr.sortingOrder = 2;

            }
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("HurtPlayer") && shouldJump)
        {
            //Destroy(jumpT);
            jumpT.SetActive(false);
            shouldJump = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(JumpUp());
        }
    }

}
