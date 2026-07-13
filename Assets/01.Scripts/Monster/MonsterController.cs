using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] float moveSpeed = 3f;
    
    Vector2 moveDir;

    Rigidbody2D rb;

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
        moveDir = target.position - transform.position;
        moveDir = moveDir.normalized;
    }

    private void Move()
    {
        rb.linearVelocity = moveDir * moveSpeed;
    }
}
