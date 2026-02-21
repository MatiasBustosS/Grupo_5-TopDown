using GameKits.HealthSystem.Scripts;
using GameKits.InventorySystem.ScriptableObjects;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterInfo : MonoBehaviour, IDamagable
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private HealthManagerUI healthManagerUI;

    [Header("Combat")]
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown = 1f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [HideInInspector] public UnityEvent OnDie;

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;
    public int Damage => damage;
    public float AttackCooldown => attackCooldown;
    public float MoveSpeed => moveSpeed;
    public bool IsDeath => isDeath;
    public int CurrentDamage {
        set => currentDamage = value;
        get => currentDamage; }

    private static readonly WaitForSeconds _waitForSeconds0_5 = new(0.5f);
    private int currentDamage;
    private bool isDeath;
    private int hitCount;
    private int minHitsToAnimate = 1;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            minHitsToAnimate = 3;
        }
    }

    public void TakeDamage(int amount)  
    {
        currentHealth -= amount;

        healthManagerUI.UpdateBar(maxHealth, currentHealth);

        hitCount++;

        if(hitCount >= minHitsToAnimate)
        {
            GetComponentInChildren<Animator>().SetTrigger("Hit");
            hitCount = 0;
        }


        if (currentHealth <= 0)
        {
            isDeath = true;
            OnDie.Invoke();
        }

        StartCoroutine(CheckDeath());
    }

    private IEnumerator CheckDeath()
    {
        yield return _waitForSeconds0_5;

        if (IsDeath)
        {
            isDeath = true;
        }
    }

    public void RestoreHealth(ItemData itemData)
    {
        currentHealth += (int)itemData.attribute;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthManagerUI.UpdateBar(maxHealth, currentHealth);
    }

    public void Restart()
    {
        isDeath = false;
        currentHealth = maxHealth;
        currentDamage = damage;
        healthManagerUI.UpdateBar(maxHealth, currentHealth);
    }
}
