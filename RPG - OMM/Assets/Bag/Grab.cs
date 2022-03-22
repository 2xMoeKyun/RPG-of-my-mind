using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private Bag bag;
    public GameObject bagSlot;
    private void Start()
    {
        bag = GameObject.Find("Bag").GetComponent<Bag>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < bag.BagSlots.Length; i++)
            {
                if (bag.isBagFull[i] == false)
                {
                    bagSlot.SetActive(true);
                    bag.isBagFull[i] = true;
                    Instantiate(bagSlot, bag.BagSlots[i].transform);
                    Destroy(gameObject);
                    break;
                }
            }

        }
    }
}
