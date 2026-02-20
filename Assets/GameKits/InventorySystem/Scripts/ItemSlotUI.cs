using GameKits.InventorySystem.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameKits.InventorySystem.Scripts
{
    public class ItemSlotUI : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI quantityText;

        [HideInInspector] public UnityEvent<Item> OnClickEvent;

        private Item item;

        private void OnDestroy()
        {
            OnClickEvent.RemoveAllListeners();
        }

        public void UpdateItemSlot(Item item)
        {
            this.item = item;
            iconImage.sprite = item.itemData.icon;
            nameText.text = item.itemData.name;
            quantityText.text = $"x{item.quantity}";
        }

        public void OnClick()
        {
            if (this.item != null && item.itemData.type == ItemData.ItemType.Consumable)
            {
                OnClickEvent.Invoke(item);
            }
        }
    }
}