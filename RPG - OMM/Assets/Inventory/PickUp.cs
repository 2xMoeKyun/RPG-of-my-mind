using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Inventory inventory;
    public GameObject slotButton;
    public static bool isKey;
    GameObject a;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }



    public static bool delete = false;
    private void Update()
    {
        if (delete)
        {
            delete = false;
            Destroy(GameObject.Find("KeyOnInv(Clone)"));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (slotButton.CompareTag("Key"))
            {
                isKey = true;
            }
            if (gameObject.CompareTag("RedStone"))
            {
                inventory.isFull[0] = true;
                a = Instantiate(slotButton, inventory.slots[0].transform);
                Destroy(gameObject);
                Move.CanUse = true;
            }
            else
            {
                for (int i = 1; i < inventory.slots.Length; i++)
                {
                    if (inventory.isFull[i] == false)
                    {
                        slotButton.SetActive(true);
                        inventory.isFull[i] = true;
                        Instantiate(slotButton, inventory.slots[i].transform);
                        Destroy(gameObject);
                        Move.CanUse = true;
                        break;
                    }
                }
            }
        }
    }
}
