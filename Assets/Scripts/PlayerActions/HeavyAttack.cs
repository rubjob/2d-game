using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttack : BaseEntityState
{
    private void Start()
    {
        Setup();
    }

    private void Update() {}

    protected override void Action(GameObject[] targets)
    {
        if (targets.Length > 0)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].GetComponent<HealthScript>().TakeDamage(attackDamage);
            }
        }
    }
}
