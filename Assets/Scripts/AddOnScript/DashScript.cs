using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScript : MonoBehaviour
{
    public Rigidbody2D rb;


    public float dashSpeedMultipiler = 5f;
    public float dashDuration = 0.7f;
    public float dashCooldown = 0.5f;

    private float nextDash;
    private bool isDashing = false;
    private PlayerController pc;

    void Start()
    {
        nextDash = -dashCooldown;
        pc = gameObject.GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        if (nextDash - Time.time < dashCooldown)
        {
            isDashing = false;
        }
    }

    public void Dash()
    {
        float currentTime = Time.time;
        if (currentTime > nextDash + dashCooldown)
        {
            isDashing = true;
            nextDash = currentTime + dashDuration;

            if (rb.velocity == Vector2.zero)
            {
                rb.velocity = pc.spriteRenderer.flipX ? new Vector2(-pc.movementSpeed, 0) : new Vector2(pc.movementSpeed, 0);
            }

            rb.velocity *= dashSpeedMultipiler;

        }
    }

    public bool IsDashing
    {
        get { return isDashing; }
    }
}
