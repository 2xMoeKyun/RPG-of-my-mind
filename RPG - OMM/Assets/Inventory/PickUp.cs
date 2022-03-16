using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Inventory inventory;
    public GameObject slotButton;
    public int currentIndex;
    GameObject currentObject;
    public static bool isKey;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    public void DeleteSlotButton()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
            currentIndex = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentIndex = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentIndex = 2;
        }
        currentObject = inventory.slots[currentIndex];
        if (currentObject.transform.GetComponentInChildren<Transform>().CompareTag("Key"))
        {
            Debug.Log("dfsafds");
            isKey = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.CompareTag("RedStone"))
            {
                inventory.isFull[0] = true;
                Instantiate(slotButton, inventory.slots[0].transform);
                Destroy(gameObject);
                Move.CanUse = true;
            }
            else
            {
                for (int i = 1; i < inventory.slots.Length; i++)
                {
                    if (inventory.isFull[i] == false)
                    {
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
