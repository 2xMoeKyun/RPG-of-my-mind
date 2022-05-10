using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{

    [Header("Scene index")]
    public int loadSceneNumber;
    [Header("Move GameObjects To Scene")]
    public GameObject[] Objects;

    private Bag bag;

    private void Start()
    {
        bag = GameObject.FindGameObjectWithTag("Bag").GetComponent<Bag>();
        TransitionManager._bagSlots = new RectTransform[bag.BagSlots.Length];
        TransitionManager._isBagFull = new bool[bag.isBagFull.Length];
    }

    public void TransitPlayerSaves()
    {
        for (int i = 0; i < bag.BagSlots.Length; i++)
        {
            if (TransitionManager._isBagFull[i] == false && bag.BagSlots[i].transform.GetChildCount() == 2)
            {

                TransitionManager._bagSlots[i] = Instantiate(bag.BagSlots[i].transform.GetChild(1)) as RectTransform;
                TransitionManager._isBagFull[i] = true;
                Debug.Log(TransitionManager._bagSlots[0] == null);
            }
        }
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
