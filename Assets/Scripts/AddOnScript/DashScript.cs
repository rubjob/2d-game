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

    void Start()
    {
        nextDash = -dashCooldown;
    }

    void FixedUpdate()
    {
        if (nextDash - Time.time < dashCooldown) {
            isDashing = false;
            rb.velocity = Vector2.zero;
        }
    }

    public void Dash()
    {
        float currentTime = Time.time;
        if (currentTime > nextDash + dashCooldown)
        {
            isDashing = true;
            nextDash = currentTime + dashDuration;

            if (rb.velocity == Vector2.zero) {
                // rb.velocity = sr.flipX ? new Vector2(-movementSpeed, 0) : new Vector2(movementSpeed, 0);
            }
            else {
                rb.velocity *= dashSpeedMultipiler;
            }
        }
    }

    public bool IsDashing {
        get { return isDashing; }
    }
}
