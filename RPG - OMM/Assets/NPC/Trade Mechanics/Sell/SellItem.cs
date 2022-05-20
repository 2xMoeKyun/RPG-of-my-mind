using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellItem : MonoBehaviour
{
    public int index;

    public static GameObject[] sellItem;
    public static bool[] isActive;
    public static int[] isPresseds;

    public static int totalCoins;

    public bool isPressed;

    public Sell sell;
    public SlotManager sm;
    private void Start()
    {
        sellItem = new GameObject[20];
        isActive = new bool[sellItem.Length];
        isPresseds = new int[sellItem.Length];
    }



    public void Sell()
    {
        sm.isSlotFull[index] = true;
        if (isPresseds[index] == 3)
        {
            isPressed = false;
        }
        if (!isPressed && transform.GetChildCount() != 0)
        {
            Debug.Log(1);
            isPresseds[index] = 1;
            isActive[index] = true;
            sellItem[index] = gameObject;
            sellItem[index].GetComponent<Image>().color = Color.red;
            totalCoins += sellItem[index].transform.GetChild(0).GetComponent<BagItemUI>().Cost;
            sell.totalCoins.text = totalCoins.ToString();
            isPressed = true;
        }
        else if(isPressed && transform.GetChildCount() != 0)
        {
            Debug.Log(2);
            isPresseds[index] = 0;
            isActive[index] = false;
            sellItem[index].GetComponent<Image>().color = new Color32(91, 88, 88, 255);
            totalCoins -= sellItem[index].transform.GetChild(0).GetComponent<BagItemUI>().Cost;
            sellItem[index] = null;
            sell.totalCoins.text = totalCoins.ToString();
            isPressed = false;
        }
    }
}
