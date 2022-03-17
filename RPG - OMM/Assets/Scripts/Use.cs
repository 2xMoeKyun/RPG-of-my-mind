using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Use : MonoBehaviour
{
    public Transform SpawnObject;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Move.isUsed && PickUp.isKey)
        {
            Instantiate(SpawnObject, gameObject.transform.position , Quaternion.identity);
            Move.isUsed = false;
            PickUp.delete = true;
            Destroy(gameObject);
        }
    }
}
