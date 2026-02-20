using System;
using UnityEngine;

public class MakeDamage : MonoBehaviour
{
    [SerializeField] private CharacterInfo _characterInfo;
    private CharacterInfo another;
    private bool isPlayer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            another = other.gameObject.GetComponent<CharacterInfo>();
            isPlayer = true;
        }
        
        else if (other.CompareTag("Enemy"))
        {
            another = other.gameObject.GetComponent<CharacterInfo>();
            isPlayer = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            another = null;
        }
        
        else if (other.CompareTag("Enemy"))
        {
            another = null;
        }
    }

    public void Damage()
    {
        if (another != null)
        {
            another.TakeDamage(_characterInfo.DamageActual);
        }
    }
}
