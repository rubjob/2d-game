using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void OnTakenDamage(float amount)
    {
        Debug.Log("Damaged taken: " + amount);
        animator.SetTrigger("isDamaged");
    }

    public void OnDead()
    {
        Debug.Log("IM DEAD ARRRRRRRRRRRRRRRRRRRRRR");
    }
}
