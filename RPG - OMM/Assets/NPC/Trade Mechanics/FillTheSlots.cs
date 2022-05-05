using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillTheSlots : MonoBehaviour
{
    private SlotManager sm;
    private Bag bag;
    private void Start()
    {
        sm = GetComponent<SlotManager>();
        bag = GameObject.FindGameObjectWithTag("Bag").GetComponent<Bag>();
    }
  
    public void Fill()// width = 51 height = 65
    {
        Debug.Log("Cycelfor");
        for (int i = 0; i < sm.Slots.Length; i++)
        {
            
            if (sm.isSlotFull[i] == false)
            {
                
                sm.isSlotFull[i] = true;
                Instantiate(bag.BagSlots[i], sm.Slots[i].transform);
                break;
            }
        }
    }
}
