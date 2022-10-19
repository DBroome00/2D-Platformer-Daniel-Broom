using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{

    public float ProjectileSpeed = 30;
    public int attackDamage = 10;
    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * ProjectileSpeed;
        Destroy(gameObject, .5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject targetHit = collision.gameObject;
        if (targetHit.tag == "Enemy")
            targetHit.GetComponent<EnemyHealth>().TakeDamage(1);

        Destroy(gameObject);
    }
}
