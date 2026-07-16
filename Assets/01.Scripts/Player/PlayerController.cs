using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;

    [SerializeField] private float atkPower;
    [SerializeField] private float atkDelay;

    [SerializeField] private float moveSpeed;

    private Vector2 moveDir;

    [SerializeField] Weapon weapon;

    private Rigidbody2D rb;

    private SpriteRenderer sr;

    public event Action<float> AtkPowerChanged;
    public event Action<float> AtkDelayChanged;

    public float AtkPower => atkPower;
    public float AtkDelay => atkDelay;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        InitializeStats();
    }

    void Update()
    {
        SetMoveDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void InitializeStats()
    {
        maxHp = 20f;
        SetHp(maxHp);

        SetAtkPower(3f);
        SetAtkDelay(2f);

        moveSpeed = 3f;
    }

    private void SetMoveDirection()
    {
        moveDir = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
        {
            moveDir += Vector2.up;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            moveDir += Vector2.down;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            moveDir += Vector2.left;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            moveDir += Vector2.right;
        }

        moveDir = moveDir.normalized;

        if (moveDir.x < 0)
        {
            sr.flipX = true;
        }
        else if (moveDir.x > 0)
        {
            sr.flipX = false;
        }
    }

    private void Move()
    {
        rb.linearVelocity = moveDir * moveSpeed;
    }

    public void TakeDamage(float damage)
    {
        float finalHp = currentHp - damage;

        if (finalHp <= 0)
        {
            finalHp = 0;
            Die();
        }

        SetHp(finalHp);
    }

    private void Die()
    {

    }

    private void SetHp(float finalHp)
    {
        currentHp = finalHp;
    }

    private void SetAtkPower(float finalAtkPower)
    {
        atkPower = finalAtkPower;
        AtkPowerChanged?.Invoke(atkPower);
    }
    private void SetAtkDelay(float finalAtkDelay)
    {
        atkDelay = finalAtkDelay;
        AtkDelayChanged?.Invoke(atkDelay);
    }

}
