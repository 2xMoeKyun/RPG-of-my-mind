using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillTheSlots : MonoBehaviour
{
    private SlotManager sm;
    public GameObject currentSlot;
    private void Start()
    {
        sm = GetComponent<SlotManager>();
    }
  
    public void Fill()
    {
        for (int i = 0; i < sm.Slots.Length; i++)
        {
            if (sm.isSlotFull[i] == false)
            {
                sm.isSlotFull[i] = true;
                Instantiate(currentSlot, sm.Slots[i].transform);
                break;
            }
        }
    }
}
