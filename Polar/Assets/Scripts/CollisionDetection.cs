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
    public Camera cam;

    private void Awake()
    {
        groundCheck = transform.GetChild(0);

    }

    void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, 0.2f, killLayer) && groundCheck.gameObject.activeSelf)
        {
            //transform.position = new Vector2(respawnCoords.x,respawnCoords.y);   **could use if we add respawn checkpoints
            if (otherPlayer.gameObject.activeSelf)
            {
                uim.LoseHeart();
                Debug.Log("only 2");
                StartCoroutine(SafeRespawn());
            }
            else
            {
                GameManager.Instance.GameOver("Only one of you made it out");
            }
        }
    }

    private IEnumerator SafeRespawn()                                          // Ensures the other player is grounded to respawn them there
    {
        if (otherPlayer.GetComponent<PlayerController>().isGrounded)
        {
            transform.position = otherPlayer.transform.position;

        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
            groundCheck.gameObject.SetActive(false);
            cam.GetComponent<MultipletargetCamera>().targets.Remove(transform);
            while (!groundCheck.gameObject.activeSelf)
            {
                if (otherPlayer.GetComponent<PlayerController>().isGrounded)
                {
                    transform.position = otherPlayer.transform.position;
                    cam.GetComponent<MultipletargetCamera>().targets.Add(transform);
                    groundCheck.gameObject.SetActive(true);
                    GetComponent<SpriteRenderer>().enabled = true;
                }
                yield return null;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Breakable"))                     // Collison with a breakable object
        {
            if (collision.gameObject.GetComponent<Crackable>().isIcey == true)
            {
                GetComponent<PlayerController>().isOnIce = true;
            }
            collision.gameObject.GetComponent<Crackable>().StartCrack();
        }
        else if (collision.gameObject.CompareTag("SelectiveBreakable"))      // Collison with selective breakable object
        {
            if (collision.gameObject.GetComponent<ThinIce>().isIcey == true)
            {
                GetComponent<PlayerController>().isOnIce = true;
            }
            collision.gameObject.GetComponent<ThinIce>().BreakCheck(gameObject);
        }
        else if (collision.gameObject.tag.Equals("Pushable"))               // Collison with a pushable object
        {

            Vector3 hit = collision.GetContact(0).normal;
            float angle = Vector3.Angle(hit, Vector3.up);

            if (angle >= 0 && angle <= 10)                                  // colliding from top (ride moving object)
            {
                GetComponent<PlayerController>().Riding = collision.gameObject;
            }
            else if (angle >= 90 && angle <= 100)                           // colliding from sides (push object)
            {
                collision.transform.GetComponent<Pushable>().Pushed(gameObject);
            }
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Breakable"))                   // stopped colliding with breakable object
        {
            GetComponent<PlayerController>().isOnIce = false;
        }
        else if (collision.gameObject.tag.Equals("Pushable"))               // stopped colliding with pushable object
        {
            GetComponent<PlayerController>().Riding = null;
        }
        else if (collision.gameObject.CompareTag("SelectiveBreakable"))     // stopped colliding with selective breakable object
        {
            GetComponent<PlayerController>().isOnIce = false;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))                        // Check if collecting fish
        {
            Destroy(collision.gameObject);
            GameManager.Instance.numOfFish++;
        }

        else if (collision.gameObject.name == otherPlayer.name && collision.gameObject != GetComponent<PlayerController>().cannotRide)  // riding mom bear
        {
            GetComponent<PlayerController>().Riding = collision.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == otherPlayer.name)                  // Stopped riding mom bear
        {
            GetComponent<PlayerController>().Riding = null;
        }
    }
}
