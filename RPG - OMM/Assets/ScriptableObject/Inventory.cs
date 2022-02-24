using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Item> StartItems = new List<Item>();
    public List<Item> inventoryItems = new List<Item>();

    void Start()
    {
        for(var i = 0; i < StartItems.Count; i++)
        {
            AddItem(StartItems[i]);
        }
    }

    public void AddItem(Item item)
    {
        inventoryItems.Add(item);
    }
}
