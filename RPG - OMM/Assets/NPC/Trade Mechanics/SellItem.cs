using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellItem : MonoBehaviour
{
    public int index;

    public static GameObject[] sellItem;
    public static bool[] isActive;

    public static int totalCoins;

    private bool isPressed;

    private Sell sell;
    private void Start()
    {
        sellItem = new GameObject[20];
        isActive = new bool[sellItem.Length];
        sell = GameObject.FindGameObjectWithTag("Sell").GetComponent<Sell>();
    }

    public void Sell()
    {
        if (!isPressed && transform.GetChildCount() != 0)
        {
            isActive[index] = true;
            sellItem[index] = gameObject;
            sellItem[index].GetComponent<Image>().color = Color.red;
            totalCoins += sellItem[index].transform.GetChild(0).GetComponent<BagItemUI>().Cost;
            sell.totalCoins.text = totalCoins.ToString();
            isPressed = true;
        }
    }
}
