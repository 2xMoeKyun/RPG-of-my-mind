using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public bool[] isBagFull;
    public GameObject[] BagSlots;


    public GameObject Slots;
    private bool isActive = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SlotsAtcive();
        }
    }


    public void TakeItem(GameObject Item)// BuyItem
    {
        for (int i = 0; i < BagSlots.Length; i++)
        {
            if (isBagFull[i] == false)
            {
                isBagFull[i] = true;
                Item.SetActive(true);
                Instantiate(Item, BagSlots[i].transform);
                break;
            }
        }
    }


    public void SlotsAtcive()
    {
        isActive = !isActive;
        Slots.SetActive(isActive);
    }
}
