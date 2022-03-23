using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OpenChest : MonoBehaviour
{
    public GameObject[] items; 
    Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim = GetComponent<Animator>();
            anim.SetTrigger("Opening");
            for(int i = 0; i < items.Length; i++)
            {
                Instantiate(items[i], new Vector2(collision.transform.position.x - i , collision.transform.position.y), Quaternion.identity);
            }
            Destroy(this);
        }
    }
}
