using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTriggerObject : MonoBehaviour
{
    public static string parentName;


    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && !NPC.EndOfCountDialogue && Move.fUsed)
        {
            NPC.npcDialogue = true;
            transform.parent.GetComponent<NPC>().isDialogue = true;
            parentName = transform.parent.name;
            collision.GetComponent<Move>().DisablePlayer();
        }
        if (collision.CompareTag("Player") && NPC.EndOfCountDialogue)
        {
            collision.GetComponent<Move>().AblePlayer();
            Debug.Log("AblePLayer");
            parentName = null;
            Move.fUsed = false;
        }
    }
}
