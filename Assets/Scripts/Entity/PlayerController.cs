using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseEntity
{
    [Header("Player Controller")]
    public float movementSpeed = 5f;
    public float dashSpeedMultipiler = 5f, dashDuration;

    [Header("Mouse Util")]
    public MouseUtil mouseUtil;

    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;
    private float nextDash;
    private bool isDashing;
    private void Start()
    {
        Setup();
        nextDash = -0.5f;
        isDashing = false;
        dashDuration = 0.7f;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

        if (nextDash - Time.time < 0.5f)
        {

            // Debug.Log(Time.time - nextDash);

            isDashing = false;
            rb.velocity = Vector2.zero;
        }
        // Movement
        if (!isDashing)
        {
            velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            velocity = velocity.normalized * movementSpeed;
            rb.velocity = velocity;
        }

        // Play animation
        velocity = GetComponent<Rigidbody2D>().velocity;
        bool isMoving = velocity != Vector2.zero;

        animator.SetBool("isMoving", isMoving);
        if (isMoving && velocity.x != 0) {
            spriteRenderer.flipX = velocity.x < 0;
        }

        // Combat
        UpdateHitBox(mouseUtil.GetMouseAngle());

        if (Input.GetAxis("Fire1") == 1)
            Attack(BindingState.PrimaryAttack);

        else if (Input.GetAxis("Fire2") == 1)
            Attack(BindingState.HeavyAttack);

        else if (Input.GetAxis("Jump") == 1 && Time.time > nextDash + 0.5f)
            Dash();
    }
    public void LockMovement() {
        float attackSpeed = GetCurrentState().EntityState.attackSpeed;
        animator.speed = (attackSpeed >= 1) ? attackSpeed : 1f;
        movementSpeed = 0;
    }

    public void UnlockMovement() {
        animator.speed = 1f;
        movementSpeed = 5f;

        //reset animation condition
        animator.SetBool("isAttackingSide", false);
        animator.SetBool("isAttackingDown", false);
        animator.SetBool("isAttackingUp", false);
    }

    private void Dash()
    {
        isDashing = true;
        nextDash = Time.time + dashDuration;
        if (rb.velocity == Vector2.zero)
        {
            // rb.velocity = sr.flipX ? new Vector2(-movementSpeed, 0) : new Vector2(movementSpeed, 0);
        }
        else
        {
            rb.velocity *= dashSpeedMultipiler;
        }
    }

    protected override void OnAttacking()
    {
        float mAngle = mouseUtil.GetMouseAngle();
        if (mAngle <= 45 && mAngle >= -45) {
            //attack right animation
            spriteRenderer.flipX = false;
            animator.SetBool("isAttackingSide", true);
        }
        else if (mAngle >= 135 || mAngle <= -135) {
            //attack left animation
            spriteRenderer.flipX = true;
            animator.SetBool("isAttackingSide", true);
        }
        else if (mAngle < -45 && mAngle > -135) {
            //attack down animation
            animator.SetBool("isAttackingDown", true);
        }
        else if (mAngle > 45 && mAngle < 135) {
            //attack up animation
            animator.SetBool("isAttackingUp", true);
        }
    }
    protected override void OnTakenDamage(float amount) { }
    protected override void OnDead() { }
}
