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

    private Sell sell;
    public SlotManager sm;
    private void Start()
    {
        sellItem = new GameObject[20];
        isActive = new bool[sellItem.Length];
        isPresseds = new int[sellItem.Length];
        sell = GameObject.FindGameObjectWithTag("Sell").GetComponent<Sell>();
    }



    public void Sell()
    {
        if(isPresseds[index] == 3)
        {
            isPressed = false;
        }
        if (!isPressed && transform.GetChildCount() != 0)
        {
            isPresseds[index] = 1;
            isActive[index] = true;
            sm.isSlotFull[index] = true;
            sellItem[index] = gameObject;
            sellItem[index].GetComponent<Image>().color = Color.red;
            totalCoins += sellItem[index].transform.GetChild(0).GetComponent<BagItemUI>().Cost;
            sell.totalCoins.text = totalCoins.ToString();
            isPressed = true;
        }
        else if(isPressed && transform.GetChildCount() != 0)
        {
            isPresseds[index] = 0;
            isActive[index] = false;
            sm.isSlotFull[index] = false;
            sellItem[index].GetComponent<Image>().color = new Color32(91, 88, 88, 255);
            totalCoins -= sellItem[index].transform.GetChild(0).GetComponent<BagItemUI>().Cost;
            sellItem[index] = null;
            sell.totalCoins.text = totalCoins.ToString();
            isPressed = false;
        }
    }
}
