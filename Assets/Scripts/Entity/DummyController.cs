using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : BaseEntity
{
    private void Start()
    {
        Setup();
    }

    protected override void OnPerformingAction() { }

    protected override void OnTakenDamage(float amount)
    {
        Debug.Log("Damaged taken: " + amount);
        animator.SetTrigger("isDamaged");
    }

    protected override void OnDead()
    {
        Debug.Log("IM DEAD ARRRRRRRRRRRRRRRRRRRRRR");
    }
}
