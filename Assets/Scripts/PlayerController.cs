using GameKits.InventorySystem.ScriptableObjects;
using GameKits.InventorySystem.Scripts;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MovementController
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference attack;
    [SerializeField] private InputActionReference interact;

    private static readonly WaitForSeconds _waitForSeconds0_5 = new(0.5f);
    private Animator animator;
    private Vector2 lastInput;
    private bool interactuando;
    private float lastAttackTime;

    public bool Interactuando { get => interactuando; set => interactuando = value; }

    public static PlayerController Instance;

    protected override void Awake()
    {
        if(Instance == null)
        {
            base.Awake();
            animator = GetComponent<Animator>();
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        InventoryManager.instance.OnConsumeEvent.AddListener(OnConsume);
    }

    private void OnEnable()
    {
        attack.action.started += Attack;
        interact.action.started += Interact;
    }

    private void Update()
    {
        lastAttackTime += Time.deltaTime;

        if (!interactuando)
        {
            moveTo = move.action.ReadValue<Vector2>();

            if(moveTo != Vector2.zero)
            {
                lastInput = moveTo;
            }

            SetAnimation();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = (Vector3)lastInput.normalized;
        Gizmos.DrawWireSphere(GetAttackCircle(), 0.5f);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        attack.action.started -= Attack;
        interact.action.started -= Interact;
        InventoryManager.instance.OnConsumeEvent.RemoveListener(OnConsume);
    }

    protected override void OnDie()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        StartCoroutine(RespawnPlayer());
    }

    private IEnumerator RespawnPlayer()
    {
        yield return _waitForSeconds0_5;

        Restart();
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        if(lastAttackTime < info.AttackCooldown)
        {
            return;
        }

        lastAttackTime = 0;

        animator.SetTrigger("Attack");

        Collider2D hit = Physics2D.OverlapCircle(GetAttackCircle(), 0.5f, LayerMask.GetMask("Enemy"));

        if (hit != null && hit.CompareTag("Enemy") && hit.TryGetComponent<CharacterInfo>(out var characterInfo))
        {
            characterInfo.TakeDamage(info.Damage);
        }
    }

    private void SetAnimation()
    {
        if(moveTo.x != 0 || moveTo.y != 0)
        {
            animator.SetBool("IsWalking", true);
            animator.SetFloat("InputH", moveTo.x);
            animator.SetFloat("InputV", moveTo.x != 0 ? 0 : moveTo.y);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    private Vector3 GetAttackCircle()
    {
        Vector3 direction = (Vector3)lastInput.normalized;

        return transform.position + direction * 0.5f;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        StartInteraction();
    }

    private void StartInteraction()
    {
        var npcCollider = Physics2D.OverlapCircle(GetAttackCircle(), 0.5f, LayerMask.GetMask("NPC"));

        if (npcCollider && npcCollider.gameObject.TryGetComponent<NPC>(out NPC npcScript))
        {
            npcScript.Interactuar();
        }
    }

    private void OnConsume(ItemData itemData)
    {
        info.RestoreHealth(itemData);
    }
}
