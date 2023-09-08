using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseEntity
{
    [Header("Player Controller")]
    public float movementSpeed = 5f;

    [Header("Mouse Util")]
    public MouseUtil mouseUtil;

    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;

    private BaseEntityState playerState;
    private void Start()
    {
        Setup();

        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Movement
        velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        velocity = velocity.normalized * movementSpeed;
        rb.velocity = velocity;

        // Combat
        UpdateHitBox(mouseUtil.GetMouseAngle());

        if (Input.GetAxis("Fire1") == 1)
            Attack(BindingState.PrimaryAttack);

        else if (Input.GetAxis("Fire2") == 1)
            Attack(BindingState.HeavyAttack);
    }

    protected override void OnTakenDamage(float amount) { }
    protected override void OnDead() { }
}
