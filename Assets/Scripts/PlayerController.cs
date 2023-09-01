using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public int maxHealth = 100;
    private int health { get; set; }

    [Header("Player Action States")]
    public int currentState = 0;
    public GameObject[] playerStates;

    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;
    
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();

        animator = playerStates[currentState].GetComponent<Animator>();
        spriteRenderer = playerStates[currentState].GetComponent<SpriteRenderer>();

        health = maxHealth;
    }
    
    private void FixedUpdate()
    {
        // Movement
        velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        velocity = velocity.normalized * movementSpeed;

        rb.velocity = velocity;

        // Animation
        bool isMoving = velocity != Vector2.zero;
        animator.SetBool("isMoving", isMoving); 
        
        if (isMoving && velocity.x != 0) {
            spriteRenderer.flipX = velocity.x < 0;
        }

        // Combat
        if (Input.GetAxis("Fire1") == 1) {
            playerStates[currentState].GetComponent<ActionState>().PerformAction();
        }
    }
}
