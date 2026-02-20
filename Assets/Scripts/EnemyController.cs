using UnityEngine;

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
    [SerializeField] private Transform player;

    private EnemyState state = EnemyState.Idle;
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
        info.CurrentDamage = info.Damage;
    }

    protected void Update()
    {
        CheckPlayerDistance();
        
        anim.SetBool("isMoving", state == EnemyState.Patrol || state == EnemyState.Chase);

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
                anim.SetTrigger("Attack");
                break;

            case EnemyState.Hit:
                HandleHit();
                break;
        }
    }

    protected override void OnDie()
    {
        GetComponent<LootBag>()?.InstantiateLoot(transform.position);
        base.OnDie();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void HandleIdle()
    {
        moveTo = Vector2.zero;
        waitTimer += Time.fixedDeltaTime;
        
        if (waitTimer >= waitTime)
        {
            waitTimer = 0;
            state = EnemyState.Patrol;
        }
    }

    private void HandlePatrol()
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


    private void HandleChase()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        
        transform.localScale = new Vector3(Mathf.Sign(player.position.x - transform.position.x), 1, 1);
        
        moveTo = direction;

        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            state = EnemyState.Attack;
        }
    }

    private void HandleHit()
    {
        moveTo = Vector2.zero;
    }

    private void CheckPlayerDistance()
    {
        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= attackRange)
        {
            state = EnemyState.Attack;
        }
        else if (dist <= detectRange)
        {
            state = EnemyState.Chase;
        }
        else if (state == EnemyState.Chase || state == EnemyState.Attack)
        {
            state = EnemyState.Patrol;
        }
    }
}
