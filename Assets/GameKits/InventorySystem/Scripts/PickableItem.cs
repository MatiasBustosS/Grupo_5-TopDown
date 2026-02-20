using GameKits.InventorySystem.ScriptableObjects;
using UnityEngine;

namespace GameKits.InventorySystem.Scripts
{
    [RequireComponent(typeof(Sprite))]
    public class PickableItem : MonoBehaviour
    {
        [SerializeField] ItemData itemData;
        [SerializeField] SpriteRenderer sprite;

        private void Start()
        {
            sprite.sprite = itemData.icon;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                InventoryManager.instance.AddItem(itemData, 1);
                Destroy(gameObject);
            }
        }
    }
}