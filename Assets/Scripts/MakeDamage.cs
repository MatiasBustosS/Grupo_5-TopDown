using UnityEngine;

public class MakeDamage : MonoBehaviour
{
    [SerializeField] private CharacterInfo _characterInfo;

    private CharacterInfo playerInfo;
    private MovementController movementController;

    private void Awake()
    {
        movementController = GetComponentInParent<MovementController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            playerInfo = other.gameObject.GetComponent<CharacterInfo>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInfo = null;
        }
    }

    public void Damage()
    {
        if (playerInfo != null)
        {
            playerInfo.TakeDamage(_characterInfo.CurrentDamage);

            if (playerInfo.IsDeath)
            {
                movementController.Restart();
            }
        }
    }
}
