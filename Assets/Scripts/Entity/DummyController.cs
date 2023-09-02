using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : BaseEntity {
    public Animator animator;

    protected override void OnTakenDamage(float amount) {
        Debug.Log("Damaged taken: " + amount);
        animator.SetBool("isDamaged", true);
    }

    protected override void OnDead() {
        Debug.Log("IM DEAD ARRRRRRRRRRRRRRRRRRRRRR");
    }
}
