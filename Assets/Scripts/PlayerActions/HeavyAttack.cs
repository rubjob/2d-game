using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseEntityState))]
public class HeavyAttack : MonoBehaviour
{
    public PlayerController playerController;
    public float knockbackStrength = 5f;
    public float knockbackDelay = 0.15f;

    private Rigidbody2D rb;
    private BaseEntityState baseEntityState;

    private void Start()
    {
        baseEntityState = GetComponent<BaseEntityState>();
        rb = playerController.GetComponent<Rigidbody2D>();
    }

    public void Action(GameObject[] targets)
    {
        if (targets.Length > 0)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].GetComponent<HealthScript>().TakeDamage(baseEntityState.attackDamage);

                KnockbackScript kb = targets[i].GetComponent<KnockbackScript>();
                Vector2 direction = (kb.rb.position - rb.position).normalized;
                kb.Knockback(direction, knockbackStrength, knockbackDelay);
            }
        }
    }
}
