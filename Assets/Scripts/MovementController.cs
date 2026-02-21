using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterInfo))]
public class MovementController : MonoBehaviour
{
    private Rigidbody2D rb;
    protected CharacterInfo info;
    protected Vector2 moveTo;
    private Vector3 startPosition;

    [SerializeField] protected ParticleSystem deathParticle;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        info = GetComponent<CharacterInfo>();

        info.OnDie.AddListener(OnDie);

        startPosition = transform.position;
    }

    protected virtual void FixedUpdate()
    {
        Move(moveTo);
    }

    protected virtual void OnDisable()
    {
        info.OnDie.RemoveListener(OnDie);
    }

    public void Restart()
    {
        transform.position = startPosition;
        info.Restart();
    }

    private void Move(Vector2 direction)
    {
        rb.linearVelocity = direction * info.MoveSpeed;
    }

    protected virtual void OnDie()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
