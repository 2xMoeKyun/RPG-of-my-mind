using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player" && Move.isSitting)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            Move.finish = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<BoxCollider2D>().isTrigger = false;
        Move.finish = false;
    }
}
