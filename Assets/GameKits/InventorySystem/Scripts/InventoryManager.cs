using GameKits.InventorySystem.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace GameKits.InventorySystem.Scripts
{
    [RequireComponent(typeof(InventoryManagerUI))]
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager instance;

        [Header("Debug Items")]
        [SerializeField] private bool debug = false;
        [SerializeField] private List<Item> debugItems;

        [HideInInspector] public UnityEvent<ItemData> OnConsumeEvent;
        
        private InventoryManagerUI inventoryManagerUI;
        private readonly List<Item> inventory = new();

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            inventoryManagerUI = GetComponent<InventoryManagerUI>();
            inventoryManagerUI.OnConsumeEvent.AddListener(OnConsume);

            if (debug && debugItems is not null)
            {
                foreach(var item in debugItems)
                {
                    AddItem(item.itemData, item.quantity);
                }
            }
        }

        private void OnDestroy()
        {
            OnConsumeEvent.RemoveAllListeners();
        }

        public void AddItem(ItemData itemData, int quantity)
        {
            if (itemData.isStackable)
            {
                StackItem(itemData, quantity);
            }
            else
            {
                inventory.Add(new Item{ itemData = itemData, quantity = quantity });
            }

            inventoryManagerUI.RefreshInventory(inventory);
        }

        public bool TryConsume(ItemData itemData)
        {
            Item item = inventory.Find(x => x.itemData.name == itemData.name);

            if (item != null)
            {
                ConsumeItem(item);
            }

            return item != null;
        }


        private void StackItem(ItemData itemData, int quantity)
        {
            foreach (var item in inventory)
            {
                if (item.itemData.name == itemData.name)
                {
                    item.quantity += quantity;
                    return;
                }
            }

            inventory.Add(new Item { itemData = itemData, quantity = quantity });
        }

        private void OnConsume(Item item)
        {
            OnConsumeEvent.Invoke(item.itemData);
            ConsumeItem(item);
        }
        

        private void ConsumeItem(Item item)
        {
            item.quantity -= 1;

            if (item.quantity <= 0)
            {
                inventory.Remove(item);
            }

            inventoryManagerUI.RefreshInventory(inventory);
        }
    }
}