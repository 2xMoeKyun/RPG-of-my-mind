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
    public void SlotsAtcive()
    {
        isActive = !isActive;
        Slots.SetActive(isActive);
    }
}
