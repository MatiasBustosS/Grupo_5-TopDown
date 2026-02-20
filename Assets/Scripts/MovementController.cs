using System;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Rigidbody2D rb;
    protected CharacterInfo info;
    protected Vector2 moveTo;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        info = GetComponent<CharacterInfo>();
    }

    protected virtual void Update()
    {
        Move(moveTo);
    }

    void Move(Vector2 direction)
    {
        rb.linearVelocity = direction * info.MoveSpeed;
    }

}
