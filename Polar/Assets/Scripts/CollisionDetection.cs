using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public LayerMask killLayer;
    public Vector2 respawnCoords;
    public Transform groundCheck;

    // creates a circle an checks if it overplaps with object on the killLayer (water rn)
    void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, 0.2f, killLayer)) {
            transform.position = new Vector2(respawnCoords.x,respawnCoords.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Breakable"))
        {
            collision.gameObject.GetComponent<Crackable>().StartCrack();
        }
    }
}
