using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillTheSlots : MonoBehaviour
{
    public SlotManager sm;
    public Bag bag;
    private RectTransform currentObject;
    private void Start()
    {

        //bag = GameObject.FindGameObjectWithTag("Bag").GetComponent<Bag>();
    }

    public void UnFill()
    {
        for (int i = 0; i < bag.BagSlots.Length; i++)
        {
            if (sm.isSlotFull[i] == true && bag.BagSlots[i].transform.GetChildCount() == 2)
            {
                currentObject = bag.BagSlots[i].transform.GetChild(1).GetComponent<RectTransform>();
                if(currentObject.sizeDelta.x - 51 <= 0)
                {
                    break;
                }
                currentObject.sizeDelta = new Vector2(currentObject.sizeDelta.x - 51, currentObject.sizeDelta.y - 65);
            }
        }
    }


    public void Fill()// width = 51 height = 65
    {
        if (bag.BagSlots[0].transform.GetChildCount() == 2)
        {
            for (int i = 0; i < bag.BagSlots.Length; i++)
            {
                if (sm.isSlotFull[i] == false && bag.BagSlots[i].transform.childCount == 2)
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
