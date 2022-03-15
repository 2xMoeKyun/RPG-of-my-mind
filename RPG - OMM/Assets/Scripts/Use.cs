using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Use : MonoBehaviour
{
    public Transform SpawnObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Move.isUsed)
        {
            Instantiate(SpawnObject, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            Move.isUsed = false;
        }
    }
}
