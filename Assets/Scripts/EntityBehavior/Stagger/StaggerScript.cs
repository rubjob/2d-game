using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaggerScript : MonoBehaviour
{
    [Header("Constant")]
    public int AttackStaggerStrength;

    public void Attack(GameObject obj) {
        StaggerActor actor;
        if (actor = obj.GetComponent<StaggerActor>()) {
            actor.AddStaggerLevel(AttackStaggerStrength);
        }
    }
}
