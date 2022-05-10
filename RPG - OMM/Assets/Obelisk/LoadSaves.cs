using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSaves : MonoBehaviour
{
    private Bag bag;
    private Inventory inventory;
    private Coins coins;
    private void Start()
    {
        bag = GameObject.FindGameObjectWithTag("Bag").GetComponent<Bag>();
        
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        coins = GameObject.FindGameObjectWithTag("Player").GetComponent<Coins>();
        Debug.Log(TransitionManager._coinsCount);
    }

    private void Update()
    {
        //Loading bag items
        if (TransitionManager.isTransit)
        {
            if (TransitionManager._bagSlots[0] != null)
            {
                for (int i = 0; i < bag.BagSlots.Length; i++)
                {
                    if (TransitionManager._bagSlots[i] == null)
                    {
                        break;
                    }
                    else if (bag.isBagFull[i] == false)
                    {
                        Debug.Log("voshlo");
                        TransitionManager._bagSlots[i].transform.SetParent(bag.BagSlots[i].transform);
                        TransitionManager._bagSlots[i].transform.position = bag.BagSlots[i].transform.position;
                        TransitionManager._bagSlots[i].localScale = new Vector3(1, 1, 1);
                        TransitionManager._bagSlots[i] = null;
                        TransitionManager._isBagFull[i] = false;
                        bag.isBagFull[i] = true;
                    }

                }
            }
            //Loading inventory items
            if (TransitionManager._slots[0] != null)
            {
                for (int i = 0; i < inventory.slots.Length; i++)
                {
                    if (TransitionManager._slots[i] == null)
                    {
                        Debug.Log("Break");
                        break;
                    }
                    else if (inventory.isFull[i] == false)
                    {
                        Debug.Log(i);
                        TransitionManager._slots[i].transform.SetParent(inventory.slots[i].transform);
                        TransitionManager._slots[i].transform.position = inventory.slots[i].transform.position;
                        TransitionManager._slots[i].localScale = new Vector3(1, 1, 1);
                        TransitionManager._slots[i] = null;
                        TransitionManager._isFull[i] = false;
                        inventory.isFull[i] = true;
                    }

                }
            }
            //Loading coins count
            coins.coinsCount = TransitionManager._coinsCount;
            coins.UpdateCoinsCount(coins.coinsCount);
            TransitionManager._coinsCount = 0;
            TransitionManager.isTransit = false;
        }
    }
}
