using GameKits.InventorySystem.ScriptableObjects;
using GameKits.InventorySystem.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    [SerializeField] private List<Loot> items;
    [SerializeField] private GameObject pickableItemPrefab;

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        var dropItems = GetDropItems();

        foreach (Loot item in dropItems)
        {
            var pickableItem = Instantiate(pickableItemPrefab, spawnPosition, Quaternion.identity);

            pickableItem.GetComponent<PickableItem>().SetData(item.itemData, 1);
        }
    }

    private List<Loot> GetDropItems()
    {
        int randomNumber = Random.Range(0, 101);
        List<Loot> possibleItems = new();

        foreach (var item in items)
        {
            if(randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }

        return possibleItems;
    }
}
