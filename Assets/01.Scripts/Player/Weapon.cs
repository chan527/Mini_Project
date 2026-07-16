using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [SerializeField] private float atkPowerMultiplier;
    [SerializeField] private float damage;

    [SerializeField] private float atkDelayMultiplier;
    [SerializeField] private float atkDelay;

    private WaitForSeconds atkDelayWait;

    [SerializeField] private Vector2 attackAreaSize;

    [SerializeField] private LayerMask monsterLayerMask;

    private Vector2 positionOffset = new Vector2(0.35f, -0.13f);
    private float angleOffset = 70f;

    // 캐릭터가 바라보는 방향으로 공격범위를 생성하기 위한 1 or -1 값을 가지는 변수
    private int facingSign;

    private SpriteRenderer playerSr;
    private SpriteRenderer sr;

    void Start()
    {
        playerSr = playerController.GetComponent<SpriteRenderer>();
        sr = GetComponent<SpriteRenderer>();

        atkPowerMultiplier = 1f;
        atkDelayMultiplier = 1f;

        damage = playerController.AtkPower * atkPowerMultiplier;
        atkDelay = playerController.AtkDelay * atkDelayMultiplier;

        atkDelayWait = new WaitForSeconds(atkDelay);

        attackAreaSize = new Vector2(0.65f, 1.3f);

        StartCoroutine(AttackCoroutine());
    }

    void OnEnable()
    {
        playerController.AtkPowerChanged += UpdateDamage;
        playerController.AtkDelayChanged += UpdateAtkDelay;
    }

    private void OnDisable()
    {
        playerController.AtkPowerChanged -= UpdateDamage;
        playerController.AtkDelayChanged -= UpdateAtkDelay;
    }

    void LateUpdate()
    {
        ApplyPlayerFacing();
    }

    void ApplyPlayerFacing()
    {
        sr.flipX = playerSr.flipX;
        facingSign = sr.flipX ? -1 : 1;

        transform.localPosition = new Vector2(facingSign * positionOffset.x, positionOffset.y);
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, facingSign * angleOffset));
    }

    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            yield return atkDelayWait;

            Attack();
        }
    }

    public void Attack()
    {
        Debug.Log("attack");
        SwingWeapon();
        DetectMonster();
    }

    private void SwingWeapon()
    {
        // 무기 휘두르는 애니메이션 재생
    }

    private void DetectMonster()
    {
        Collider2D[] monsters = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + facingSign * (attackAreaSize.x / 2), transform.position.y),
                                attackAreaSize, 0f, monsterLayerMask);

        foreach(var monster in monsters)
        {
            monster.gameObject.GetComponent<MonsterController>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.92f, 0.012f, 0.5f);

        Gizmos.DrawCube(new Vector2(transform.position.x + facingSign * (attackAreaSize.x / 2), transform.position.y),
                                attackAreaSize);
    }

    private void UpdateDamage(float playerAtkPower)
    {
        damage = playerAtkPower * atkPowerMultiplier;
    }

    private void UpdateAtkDelay(float playerAtkDelay)
    {
        atkDelay = playerAtkDelay * atkDelayMultiplier;
        atkDelayWait = new WaitForSeconds(atkDelay);
    }
}
