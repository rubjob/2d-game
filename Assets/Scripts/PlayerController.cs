using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;

    [Header("Player Action States")]
    public int currentState = 0;
    public GameObject[] playerStates;

    [Header("Mouse Util")]
    public MouseUtil mouseUtil;

    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;
    
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private ActionState actionState;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        actionState = playerStates[currentState].GetComponent<ActionState>();
        animator = playerStates[currentState].GetComponent<Animator>();
        spriteRenderer = playerStates[currentState].GetComponent<SpriteRenderer>();

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
        actionState.hitbox.RotateTo(mouseUtil.GetMouseAngle());
        
        if (Input.GetAxis("Fire1") == 1) {
            playerStates[currentState].GetComponent<ActionState>().PerformAction(actionState.hitbox.GetTriggeringObject());
        }
    }
}
