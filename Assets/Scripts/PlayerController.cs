using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        velocity = velocity.normalized * movementSpeed;

        rb.velocity = velocity;
    }
}
