using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [Header("Health")]
    [SerializeField] private int maxHealth;

    [SerializeField] public float currentHealth;


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {   
        currentHealth -= damage;

    if (currentHealth <= 0)
    {
        Die();
    }
}

    public void Addhealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, maxHealth);
    }

    void Die()
    {
        Destroy(gameObject, 1f);

        GetComponent<Collider2D>().enabled = false;
        GetComponent<MeleeEnemy>().enabled = false;
        GetComponentInParent<EnemyPatrol>().enabled = false;
        GetComponent<Animator>().enabled = false;

    }


}
