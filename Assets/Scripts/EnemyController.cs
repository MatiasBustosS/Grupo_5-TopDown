using System;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyController : MovementController
{
    private enum EnemyState
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Hit
    }
    [SerializeField] private Animator anim;
    
    [Header("Patrol")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float waitTime = 1f;
    private int currentPointIndex = 0;

    [Header("Detection")]
    [SerializeField] private float detectRange = 5f;
    [SerializeField] private float attackRange = 1.2f;

    private EnemyState state = EnemyState.Idle;
    private Player player;
    private Vector3 targetPoint;
    private float waitTimer;

    protected override void Awake()
    {
        base.Awake();
        if (patrolPoints.Length > 0)
            targetPoint = patrolPoints[0].position;
    }

    private void Start()
    {
        info.DamageActual = info.Damage;
        player = FindFirstObjectByType<Player>().GetComponent<Player>();
    }

    protected override void Update()
    {
        base.Update();
        CheckPlayerDistance();
        
        anim.SetBool("isMoving", state == EnemyState.Patrol || state == EnemyState.Chase);

        if (state == EnemyState.Attack)
            anim.SetTrigger("Attack");
        
        switch (state)
        {
            case EnemyState.Idle:
                HandleIdle();
                break;

            case EnemyState.Patrol:
                HandlePatrol();
                break;

            case EnemyState.Chase:
                HandleChase();
                break;

            case EnemyState.Attack:
                HandleAttack();
                break;
            case EnemyState.Hit:
                HandleHit();
                break;
        }
        
    }
    
    void HandleIdle()
    {
        moveTo = Vector2.zero;
        waitTimer += Time.fixedDeltaTime;
        
        if (waitTimer >= waitTime)
        {
            waitTimer = 0;
            state = EnemyState.Patrol;
        }
    }
    
    void HandlePatrol()
    {
        if (patrolPoints.Length == 0)
            return;

        Vector2 direction = (targetPoint - transform.position).normalized;
        
        transform.localScale = new Vector3(Mathf.Sign(targetPoint.x - transform.position.x), 1, 1); 
        
        moveTo = direction;

        if (Vector2.Distance(transform.position, targetPoint) < 0.2f)
        {
            state = EnemyState.Idle;

            currentPointIndex++;

            if (currentPointIndex >= patrolPoints.Length)
                currentPointIndex = 0;

            targetPoint = patrolPoints[currentPointIndex].position;
        }
    }
    
    
    void HandleChase()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        
        transform.localScale = new Vector3(Mathf.Sign(player.transform.position.x - transform.position.x), 1, 1);
        
        moveTo = direction;

        if (Vector2.Distance(transform.position, player.transform.position) <= attackRange)
        {
            state = EnemyState.Attack;
        }
    }
    
    void HandleAttack()
    {
        moveTo = Vector2.zero;
        DealDamage();
    }
    
    public void HandleHit()
    {
        moveTo = Vector2.zero;
    }
    
    public void DealDamage()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position + transform.right * 0.5f, 0.5f, LayerMask.GetMask("Player"));
        
        
        if (hit != null)
        {
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<CharacterInfo>()?.TakeDamage(info.Damage);
            }
        }
    }
    
    
    
    void CheckPlayerDistance()
    {
        float dist = Vector2.Distance(transform.position, player.transform.position);

        if (dist <= attackRange)
        {
            state = EnemyState.Attack;
        }
        else if (dist <= detectRange)
        {
            state = EnemyState.Chase;
        }
        else if (state == EnemyState.Chase)
        {
            state = EnemyState.Patrol;
        }
    }
    
    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
    }
}
