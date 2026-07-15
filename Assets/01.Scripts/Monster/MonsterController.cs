using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] float moveSpeed = 3f;
    
    Vector2 moveDir;

    Rigidbody2D rb;

    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        SetMoveDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void SetTarget(Transform player)
    {
        target = player;
    }

    private void SetMoveDirection()
    {
        moveDir = target.position - transform.position;
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
}
