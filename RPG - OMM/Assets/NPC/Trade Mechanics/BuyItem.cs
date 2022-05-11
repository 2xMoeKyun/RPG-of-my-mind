using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    [Header("General Components")]
    public GameObject Item;
    public int Cost;

    [Header("Other")]
    public Text coinText;
    public GameObject buyButton;
    public Text playerCoins;

    private Bag bag;
    public SlotManager sm;
    private Coins coins;

    private void Start()
    {
        Destroy(Item.GetComponent<DontDestroy>());

        bag = GameObject.FindGameObjectWithTag("Bag").GetComponent<Bag>();


        coins = GameObject.FindGameObjectWithTag("Player").GetComponent<Coins>();

        coinText.text = Cost.ToString();
    }

    public void Buy()
    {
        if (coins.coinsCount >= Cost && bag.isBagFull[bag.isBagFull.Length - 1] != true)
        {
            coins.coinsCount -= Cost;
            coins.UpdateCoinsCount(coins.coinsCount);
            playerCoins.text = coins.coinsCount.ToString();

            bag.TakeItem(Item);
            for (int i = 0; i < bag.BagSlots.Length; i++)
            {
                if (sm.isSlotFull[i] == false)
                {
                    RectTransform tempItem = Item.GetComponent<RectTransform>();
                    tempItem.sizeDelta = new Vector2(70, 83);
                    GameObject TemporaryItem = Instantiate(Item, sm.Slots[i].transform);
                    TemporaryItem.SetActive(true);
                    Item.SetActive(false);
                    sm.isSlotFull[i] = true;
                    break;
                }
            }
        }
    }
}
