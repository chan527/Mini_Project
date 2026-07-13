using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 moveDir;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        SetMoveDirection();
    }

    private void FixedUpdate()
    {
        Move();
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
    }

    private void Move()
    {
        rb.linearVelocity = moveDir * moveSpeed;
    }
}
