using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDetection : MonoBehaviour
{
    //public Vector2 respawnCoords;
    public LayerMask killLayer;
    private Transform groundCheck;
    public UIManager uim;
    public GameObject otherPlayer;

    private void Awake()
    {
        groundCheck = transform.GetChild(0);

    }

    void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, 0.2f, killLayer))
        {
            //transform.position = new Vector2(respawnCoords.x,respawnCoords.y);
            if (otherPlayer.gameObject.activeSelf)
            {
                uim.LoseHeart();
                transform.position = otherPlayer.transform.position;
            }
            else
            {
                GameManager.Instance.GameOver("Only one of you made it out");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Breakable"))
        {
            if (collision.gameObject.GetComponent<Crackable>().isIcey == true)
            {
                GetComponent<PlayerController>().isOnIce = true;
            }
            collision.gameObject.GetComponent<Crackable>().StartCrack();
        }
        else if (collision.gameObject.CompareTag("SelectiveBreakable"))
        {
            if (collision.gameObject.GetComponent<ThinIce>().isIcey == true)
            {
                GetComponent<PlayerController>().isOnIce = true;
            }
            collision.gameObject.GetComponent<ThinIce>().BreakCheck(gameObject);
        }
        else if (collision.gameObject.tag.Equals("Pushable"))    //riding pushed block or pushing block
        {

            Vector3 hit = collision.GetContact(0).normal;
            float angle = Vector3.Angle(hit, Vector3.up);

            if (angle >= 0 && angle <= 10) //colliding from top
            {
                GetComponent<PlayerController>().isRiding = collision.gameObject;
            }
            else if (angle >= 90 && angle <= 100) //colliding from sides
            {
                collision.transform.GetComponent<Pushable>().Pushed(gameObject);
            }
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Breakable"))
        {
            GetComponent<PlayerController>().isOnIce = false;
        }
        else if (collision.gameObject.tag.Equals("Pushable"))
        {
            GetComponent<PlayerController>().isRiding = null;
        }
        else if (collision.gameObject.CompareTag("SelectiveBreakable"))
        {
            GetComponent<PlayerController>().isOnIce = false;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.numOfFish++;
        }

        if (collision.gameObject.name == otherPlayer.name)  //riding mom bear
        {
            GetComponent<PlayerController>().isRiding = collision.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == otherPlayer.name)
        {
            GetComponent<PlayerController>().isRiding = null;
        }
    }
}
