using UnityEngine;

public class HitCollider : MonoBehaviour
{
    [SerializeField] private MakeDamage _damage;

    public void MakeDamage()
    {
        _damage.Damage();
    }
}
