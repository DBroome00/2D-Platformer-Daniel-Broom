using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    private float timeBTWAttack;
    public float startTimeBTWAttack;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public int damage;
    public bool isAttacking;
    [SerializeField] private Animator anim;


    private void Start()
    {
        anim=GetComponent<Animator>();
    }
    void Update()
    {
        if (timeBTWAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                anim.SetTrigger("Melee");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                    
                enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);

                {

                }

                timeBTWAttack = startTimeBTWAttack;
            }
         }
        else
        {
            timeBTWAttack -= Time.deltaTime;   
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
