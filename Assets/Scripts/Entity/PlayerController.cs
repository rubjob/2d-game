using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseEntity
{
    public float movementSpeed = 5f;

    [Header("Player Action States")]
    public int currentState = 0;
    public GameObject[] playerStates;

    [Header("Mouse Util")]
    public MouseUtil mouseUtil;

    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;

    private BasePlayerState playerState;
    public HealthBar healthBar;
    private void Start()
    {
        Debug.Log(health + ", " + maxHealth);
        rb = GetComponent<Rigidbody2D>();

        healthBar.setMaxHealth(maxHealth);
    }

    private void FixedUpdate()
    {
        // Movement
        velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        velocity = velocity.normalized * movementSpeed;
        rb.velocity = velocity;

        // Combat
        playerState = playerStates[currentState].GetComponent<BasePlayerState>();
        playerState.hitbox.RotateTo(mouseUtil.GetMouseAngle());

        if (Input.GetAxis("Fire1") == 1)
        {
            // playerState.PerformAction();
        }
    }

    protected override void OnTakenDamage(float amount)
    {
        Debug.Log("Damage : "+ amount);
        healthBar.decreaseHealth(amount); 
    }

    protected override void OnHeal(float amount)
    {
        Debug.Log("Heal: "+ amount);
        healthBar.increaseHealth(amount);

    }

    protected override void OnDead()
    {

    }
}
