using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillTheSlots : MonoBehaviour
{
    private SlotManager sm;
    private Bag bag;
    private RectTransform currentObject;
    private void Start()
    {
        sm = GetComponent<SlotManager>();
        bag = GameObject.FindGameObjectWithTag("Bag").GetComponent<Bag>();
    }
  
    public void Fill()// width = 51 height = 65
    {
        if (bag.BagSlots[0].transform.GetChildCount() == 2)
        {
            for (int i = 0; i < bag.BagSlots.Length; i++)
            {
                if (sm.isSlotFull[i] == false && bag.BagSlots[i].transform.GetChildCount() == 2)
                {
                    currentObject = bag.BagSlots[i].transform.GetChild(1).GetComponent<RectTransform>();
                    currentObject.sizeDelta = new Vector2(currentObject.sizeDelta.x + 51, currentObject.sizeDelta.y + 65);
                    sm.isSlotFull[i] = true;
                    Instantiate(currentObject, sm.Slots[i].transform);
                }
            }
        }
        else
        {
            Debug.Log("Sumka soderjit null elementov");
        }
    }
}
