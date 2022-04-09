using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class From : MonoBehaviour
{
    public GameObject TeleportTo;
    public GameObject Player;
    private Vector2 from;
    

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
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.E) && PickUp.isRedStone)
        {
            gameObject.GetComponent<Animator>().SetBool("Strike", true);
        }
    }

    public void Ready()
    {
        Player.GetComponent<SpriteRenderer>().enabled = false;
    }
    
    public void Going()
    {
        from = transform.position;
        transform.position = TeleportTo.transform.position;
        Player.transform.position = new Vector2(TeleportTo.transform.position.x - 0.5f, TeleportTo.transform.position.y - 0.5f);  
    }

    public void Arrived()
    {
        Player.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void End()
    {
        GetComponent<Animator>().SetBool("Strike",false);
        transform.position = from;
        
    }
}
