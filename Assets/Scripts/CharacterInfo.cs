using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterInfo : MonoBehaviour, IDamagable, IKnockbackable
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;

    [Header("Combat")]
    [SerializeField] private int damage;
    private int damageActual;
    [SerializeField] private float attackCooldown = 0.5f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    
    
    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;
    public int Damage => damage;
    public int DamageActual {
        set => damageActual = value;
        get => damageActual; }
    
    public float AttackCooldown => attackCooldown;
    public float MoveSpeed => moveSpeed;
    void Awake()
    {
        currentHealth = maxHealth;
    }

    
    public void TakeDamage(int amount)  
    {
        currentHealth -= amount;
        
        GetComponentInChildren<Animator>().SetTrigger("Hit");
        
        if (currentHealth <= 0) Destroy(gameObject);
        
        
    }

    public void ApplyKnockback(Vector2 force)
    {
        throw new System.NotImplementedException();
    }

}
