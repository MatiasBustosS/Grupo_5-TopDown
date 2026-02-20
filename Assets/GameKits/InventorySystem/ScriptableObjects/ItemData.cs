using UnityEngine;

namespace GameKits.InventorySystem.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/New Item")]
    public class ItemData : ScriptableObject
    {
        public enum ItemType
        {
            Consumable,
            Equipment,
            Key
        }

        public new string name;
        public Sprite icon;
        public string description;
        public ItemType type;
        public bool isStackable;
        public float attribute;
    }
}