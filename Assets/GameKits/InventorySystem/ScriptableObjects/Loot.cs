using UnityEngine;

namespace GameKits.InventorySystem.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Loot", menuName = "Inventory/New Loot")]
    public class Loot : ScriptableObject
    {
        public ItemData itemData;
        public int dropChance;
    }
}
