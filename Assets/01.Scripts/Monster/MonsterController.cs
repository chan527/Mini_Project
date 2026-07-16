using System.Collections;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;

    [SerializeField] private float moveSpeed;

    private Vector2 moveDir;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private WaitForSeconds wait;
    private Coroutine damageRoutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        wait = new WaitForSeconds(1f);
    }

    private void OnEnable()
    {
        damageRoutine = null;

        InitializeStats();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
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
        maxHp = 5f;
        currentHp = 5f;

        moveSpeed = 1f;
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
        ObjectPoolManager.instance.ReturnObject(name, gameObject);
    }

    private void SetHp(float finalHp)
    {
        currentHp = finalHp;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("데미지 코루틴 시작");
            var player = collision.gameObject.GetComponent<PlayerController>();
            damageRoutine = StartCoroutine(DealContactDamageCoroutine(player));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (damageRoutine != null)
            {
                Debug.Log("데미지 코루틴 정지");
                StopCoroutine(damageRoutine);
                damageRoutine = null;
            }
        }
    }

    IEnumerator DealContactDamageCoroutine(PlayerController player)
    {
        while (true)
        {
            player.TakeDamage(1f);

            yield return wait;
        }
    }
}
