using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OpenChest : MonoBehaviour
{
    public GameObject[] items; 
    private Animator anim;
    private Transform player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim = GetComponent<Animator>();
            anim.SetTrigger("Opening");
            player = collision.transform;
        }
    }


    public void ItemGive()
    {
        for (int i = 0; i < items.Length; i++)
        {
            Instantiate(items[i], new Vector2(player.position.x, player.position.y), Quaternion.identity);
        }
        Destroy(this);
    }
}
