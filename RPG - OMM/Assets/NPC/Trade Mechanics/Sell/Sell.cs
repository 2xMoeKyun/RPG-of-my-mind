using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sell : MonoBehaviour
{
    public Text totalCoins;

    private Coins coins;
    public NPCTrader nPCTrader;
    private Bag bag;
    public SlotManager sm;
    private void Start()
    {
        coins = GameObject.FindGameObjectWithTag("Player").GetComponent<Coins>();
        bag = GameObject.FindGameObjectWithTag("Bag").GetComponent<Bag>();
    }
    public void SellButtonClick()
    {
        for (int i = 0; i < bag.BagSlots.Length; i++)
        {
            if (SellItem.isActive[i] == true)
            {
                sm.isSlotFull[i] = false;
                SellItem.isPresseds[i] = 3;
                //
                Destroy(SellItem.sellItem[i].transform.GetChild(0).gameObject);
                SellItem.sellItem[i].GetComponent<Image>().color = new Color32(91, 88, 88, 255);
                SellItem.sellItem[i] = null;
                //
                
                Destroy(bag.BagSlots[i].transform.GetChild(1).gameObject);
                bag.isBagFull[i] = false;
                //
                SellItem.isActive[i] = false;
            }
        }
        //
        coins.UpdateCoinsCount(coins.coinsCount += SellItem.totalCoins);
        nPCTrader.coinText.text = coins.coinsCount.ToString();
        SellItem.totalCoins = 0;
        totalCoins.text = "0";
        //
    }
}
