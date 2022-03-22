using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public bool[] isBagFull;
    public GameObject[] BagSlots;


    public GameObject Slots;
    private bool isActive = false;
    public void SlotsAtcive()
    {
        isActive = !isActive;
        Slots.SetActive(isActive);
    }
}
