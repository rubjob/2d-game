using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMover : MonoBehaviour
{
    [Header("Dependency")]
    public Rigidbody2D rb;

    [Header("Player Movement")]
    public float BaseMovementSpeed = 5f;
    private float AdditionalMovementSpeed = 0f;
    public float MovementSpeed { get => BaseMovementSpeed + AdditionalMovementSpeed; }
    public bool IsBlockingMovement { get; set; }

    public void MoveToDirection(Vector2 direction) {
        if (IsBlockingMovement) return;

        rb.velocity = direction.normalized * MovementSpeed;
    }

    public void SetAdditionalMovementSpeed(float Amount) {
        AdditionalMovementSpeed = Amount;
    }
}
