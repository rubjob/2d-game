using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackScript : MonoBehaviour
{
    [Header("Knockback")]
    public float[] knockbackStrength = { 5f };
    public float[] knockbackDelay = { 0.15f };

    public int Index { set; private get; } = 0;

    public void Knockback(GameObject obj, Vector2 direction) {
        KnockbackActor actor;
        if (actor = obj.GetComponent<KnockbackActor>()) {
            actor.Knockback(direction.normalized, knockbackStrength[Index], knockbackDelay[Index]);
        }
    }
}
