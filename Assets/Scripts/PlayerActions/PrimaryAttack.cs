using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryAttack : ActionState
{
    protected override void Action(GameObject target) {
        Debug.Log("PRIMARY ATTACKING to " + target.name);
    }
}
