using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goombastomp : MonoBehaviour
{
    public float bounce;
    public Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Destroy(other.gameObject);
            rb.velocity = new Vector2(rb.velocity.x, bounce);
        }
    }
}
