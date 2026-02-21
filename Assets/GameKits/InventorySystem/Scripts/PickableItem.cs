using GameKits.InventorySystem.ScriptableObjects;
using UnityEngine;

namespace GameKits.InventorySystem.Scripts
{
    [RequireComponent(typeof(Sprite))]
    public class PickableItem : MonoBehaviour
    {
        [SerializeField] ItemData itemData;
        [SerializeField] int quantity = 1;
        [SerializeField] SpriteRenderer sprite;
        [SerializeField] ParticleSystem pickParticle;

        private void Start()
        {
            sprite.sprite = itemData.icon;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Instantiate(pickParticle, transform.position, Quaternion.identity);
                InventoryManager.instance.AddItem(itemData, quantity);
                Destroy(gameObject);
            }
        }

        public void SetData(ItemData itemData, int quantity)
        {
            this.itemData = itemData;
            this.quantity = quantity;
        }
    }
}