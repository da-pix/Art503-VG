using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class CollisionDetection : MonoBehaviour
{
    //public Vector2 respawnCoords;
    public LayerMask killLayer;
    private Transform groundCheck;
    public UIManager uim;
    public GameObject otherPlayer;
    public Camera cam;
    public float temp;
    AudioManager audioManager;

    private void Awake()
    {
        groundCheck = transform.GetChild(0);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    void FixedUpdate()
    {

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
            //groundCheck.gameObject.SetActive(false);
            GetComponent<PolygonCollider2D>().enabled = false;
            cam.GetComponent<MultipletargetCamera>().targets.Remove(transform);
            while (!GetComponent<SpriteRenderer>().enabled == true)
            {
                if (otherPlayer.GetComponent<PlayerController>().isGrounded)
                {
                    transform.position = otherPlayer.transform.position;
                    cam.GetComponent<MultipletargetCamera>().targets.Add(transform);
                    //groundCheck.gameObject.SetActive(true);
                    GetComponent<SpriteRenderer>().enabled = true;
                    GetComponent<PolygonCollider2D>().enabled = true;

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
            collision.gameObject.GetComponent<ThinIce>().BreakCheck(gameObject, GetComponent<PlayerController>().slamming);
        }
        else if (collision.gameObject.tag.Equals("Pushable"))               // Collison with a pushable object
        {

            Vector3 hit = collision.GetContact(0).normal;
            float angle = Vector3.Angle(hit, Vector3.up);

            if (angle >= 0 && angle <= 10)                                  // Colliding from top (ride moving object)
            {
                GetComponent<PlayerController>().Riding = collision.gameObject;
            }
            else if (angle >= 90 && angle <= 100)                           // Colliding from sides (push object)
            {
                collision.transform.GetComponent<Pushable>().Pushed(gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("HurtPlayer"))                  // Collison with an enemy
        {
            audioManager.PlayplayerSFX(audioManager.death);
            if (otherPlayer.gameObject.activeSelf && otherPlayer.GetComponent<SpriteRenderer>().isVisible)
            {
                uim.LoseHeart();
                StartCoroutine(SafeRespawn());
            }
            else
            {
                if (!otherPlayer.gameObject.activeSelf)
                    GameManager.Instance.GameOver("Only one bear made it out");
                else
                    GameManager.Instance.GameOver("Both bears met an oily end");
            }

        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Breakable"))                   // Stopped colliding with breakable object
        {
            GetComponent<PlayerController>().isOnIce = false;
        }
        else if (collision.gameObject.tag.Equals("Pushable"))               // Stopped colliding with pushable object
        {
            GetComponent<PlayerController>().Riding = null;
        }
        else if (collision.gameObject.CompareTag("SelectiveBreakable"))     // Stopped colliding with selective breakable object
        {
            GetComponent<PlayerController>().isOnIce = false;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))                        // Check if collecting fish
        {
            audioManager.PlaySFX(audioManager.collectFish);
            Destroy(collision.gameObject);
            GameManager.Instance.numOfFish++;
        }

        else if (collision.gameObject.name == otherPlayer.name && collision.gameObject != GetComponent<PlayerController>().cannotRide)  // Riding mom bear
        {
            GetComponent<PlayerController>().Riding = collision.gameObject;
        }
        else if (collision.gameObject.CompareTag("FakeWall"))               // Check if going into fake wall
        {
            collision.GetComponent<Tilemap>().color = new Color(1f, 1f, 1f, .35f);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.name == otherPlayer.name && collision.gameObject != GetComponent<PlayerController>().cannotRide)                  // Stopped riding mom bear
        {
            GetComponent<PlayerController>().Riding = null;
        }
        else if (collision.gameObject.CompareTag("FakeWall"))               // Left the fake wall
        {
            collision.GetComponent<Tilemap>().color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
