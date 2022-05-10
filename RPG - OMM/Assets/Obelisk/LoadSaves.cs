using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSaves : MonoBehaviour
{
    

    private Bag bag;

    private void Start()
    {
        bag = GameObject.FindGameObjectWithTag("Bag").GetComponent<Bag>();
        Debug.Log(TransitionManager._bagSlots[0]);
    }

    private void Update()
    {
        if (TransitionManager.isTransit)
        {
            if (TransitionManager._isBagFull[0] != false)
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
            TransitionManager.isTransit = false;
        }
    }
}
