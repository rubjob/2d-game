using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;
    
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void FixedUpdate()
    {
        // Movement
        velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        velocity = velocity.normalized * movementSpeed;

        rb.velocity = velocity;

        // Animation
        Boolean isMoving = velocity != Vector2.zero;
        animator.SetBool("isMoving", isMoving); 
        
        if (isMoving) {
            spriteRenderer.flipX = velocity.x < 0;
        }
    }
}
