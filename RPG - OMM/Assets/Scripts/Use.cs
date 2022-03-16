using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Use : MonoBehaviour
{
    public Transform SpawnObject;
    private PickUp pickUp;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Move.isUsed && PickUp.isKey)
        {
            Debug.Log(0);
            Instantiate(SpawnObject, gameObject.transform.position , Quaternion.identity);
            Destroy(gameObject);
            PickUp p = GetComponent<PickUp>();
            pickUp.DeleteSlotButton();
            Move.isUsed = false;
        }
    }
}
