using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    public ProjectileBehavior ProjectilePrefab;
    public Transform LaunchOffset;
    private float cooldownTimer = Mathf.Infinity;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && cooldownTimer > attackCooldown)
        {
            Instantiate(ProjectilePrefab, LaunchOffset.position, LaunchOffset.transform.rotation);
            cooldownTimer = 0;
        }
        cooldownTimer += Time.deltaTime;
    }

}
