using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMover : MonoBehaviour
{
    [Header("Dependency")]
    public Rigidbody2D rb;

    [Header("Player Movement")]
    public float BaseMovementSpeed = 5f;
    public float MovementSpeed { get; private set; }
    public bool IsBlockingMovement { get; set; }

    public EntityMover() {
        MovementSpeed = BaseMovementSpeed;
    }

    public void MoveToDirection(Vector2 direction) {
        if (IsBlockingMovement) return;

        rb.velocity = direction.normalized * MovementSpeed;
    }

    public void SetAdditionalMovementSpeed(float Amount) {
        MovementSpeed = Amount + BaseMovementSpeed;
    }
}
