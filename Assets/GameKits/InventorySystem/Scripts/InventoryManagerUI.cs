using GameKits.InventorySystem.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameKits.InventorySystem.Scripts
{
    public class InventoryManagerUI : MonoBehaviour
    {
        [SerializeField] private GameObject itemSlotPrefab;
        [SerializeField] private Transform inventoryContainer;

        [HideInInspector] public UnityEvent<Item> OnConsumeEvent;

        private void OnDestroy()
        {
            OnConsumeEvent.RemoveAllListeners();
        }

        public void RefreshInventory(List<Item> inventory)
        {
            foreach (Transform item in inventoryContainer)
            {
                Destroy(item.gameObject);
            }

            foreach (Item item in inventory)
            {
                GameObject itemSlot = Instantiate(itemSlotPrefab, inventoryContainer);
                ItemSlotUI itemSlotUI = itemSlot.GetComponent<ItemSlotUI>();
                itemSlotUI.UpdateItemSlot(item);
                itemSlotUI.OnClickEvent.AddListener(OnItemClick);
            }
        }

        private void OnItemClick(Item item)
        {
            switch (item.itemData.type)
            {
                case ItemData.ItemType.Consumable:
                    OnConsumeEvent.Invoke(item);
                    break;
                case ItemData.ItemType.Equipment:
                    break;
                default:
                    break;
            }
        }
    }
}