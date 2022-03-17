using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class From : MonoBehaviour
{
    public GameObject TeleportTo;
    public GameObject Player;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.Q))
        {
            gameObject.GetComponent<Animator>().SetTrigger("Strike");
        }
    }

    public void Ready()
    {
        Player.SetActive(false);
        
    }
    
    public void Going()
    {
        transform.position = TeleportTo.transform.position;
        Player.transform.position = TeleportTo.transform.position;
    }

    public void Arrived()
    {
        Player.SetActive(true);
        
    }

    public void End()
    {
        gameObject.SetActive(false);
    }
}
