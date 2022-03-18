using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class From : MonoBehaviour
{
    public GameObject TeleportTo;
    public GameObject Player;


    private void Update()
    {
        if(Vector2.Distance(Player.transform.position, transform.position) < 2f)
        {
            GetComponent<Animator>().SetBool("Ice", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Ice", false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.E))
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
        Player.transform.position = new Vector2(TeleportTo.transform.position.x - 0.5f, TeleportTo.transform.position.y - 0.5f);  
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
