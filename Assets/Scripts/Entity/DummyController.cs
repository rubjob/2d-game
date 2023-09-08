using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : BaseEntity
{
    public Animator animator;

    private void Start()
    {
        Setup();
    }

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
