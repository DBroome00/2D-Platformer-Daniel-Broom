using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{


    [Header ("Health")]
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

        public void Addhealth (float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, maxHealth);
    }

    void Die()
    {
        print("You died");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        currentHealth = maxHealth;
        Destroy(gameObject);
    }
}
