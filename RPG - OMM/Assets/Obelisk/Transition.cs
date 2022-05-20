using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{

    [Header("Scene index")]
    public int loadSceneNumber;

    public GameObject player;
    private Move move;
    private Bag bag;
    private Inventory inventory;
    private Coins coins;
    private void Start()
    {
        move = player.GetComponent<Move>();

        bag = GameObject.FindGameObjectWithTag("Bag").GetComponent<Bag>();
        TransitionManager._bagSlots = new RectTransform[bag.BagSlots.Length];
        TransitionManager._isBagFull = new bool[bag.isBagFull.Length];

        inventory = player.GetComponent<Inventory>();
        TransitionManager._slots = new RectTransform[inventory.slots.Length];
        TransitionManager._isFull = new bool[inventory.isFull.Length];

        coins = player.GetComponent<Coins>();
    }

    public void TransitPlayerSaves()
    {
        //saving bag items
        for (int i = 0; i < bag.BagSlots.Length; i++)
        {
            if (TransitionManager._isBagFull[i] == false && bag.BagSlots[i].transform.GetChildCount() == 2)
            {

                TransitionManager._bagSlots[i] = Instantiate(bag.BagSlots[i].transform.GetChild(1)) as RectTransform;
                TransitionManager._isBagFull[i] = true;
                Debug.Log(TransitionManager._bagSlots[0] == null);
            }
        }
        //saving inventory items
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (TransitionManager._isFull[i] == false && inventory.slots[i].transform.GetChildCount() == 2)
            {
                TransitionManager._slots[i] =  Instantiate(inventory.slots[i].transform.GetChild(1)) as RectTransform;
                TransitionManager._isFull[i] = true;
            }
        }
        //saving coins count
        if (coins.coinsCount != 0)
        {
            TransitionManager._coinsCount = coins.coinsCount;
            Debug.Log(TransitionManager._coinsCount);
        }

        TransitionManager.playerAttack = player.GetComponent<Damage>().damage;
        TransitionManager.playerHealth = player.GetComponent<Health>().health;
        TransitionManager.jumpForce = move.Jforce;
        TransitionManager.maxSpeed = move.maxSpeed;
    }

    public void TransitionTrigger(int number)
    {
        SceneManager.LoadScene(number);
        TransitionManager.SceneSwitch = true;
        Destroy(this);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !TransitionManager.isTransit)
        {
            TransitionManager.isTransit = true;
            TransitionTrigger(loadSceneNumber);
            TransitPlayerSaves();
        }
    }
}
