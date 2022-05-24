using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTriggerObject : MonoBehaviour
{
    public static string parentName;

    public static bool reloadtrade;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!transform.parent.GetComponent<NPC>().EndOfCountDialogue && Move.fUsed)
            {
                NPC.npcDialogue = true;
                transform.parent.GetComponent<NPC>().isDialogue = true;
                parentName = transform.parent.name;
                collision.GetComponent<Move>().DisablePlayer();
            }
            if (transform.parent.GetComponent<NPC>().EndOfCountDialogue)
            {
                collision.GetComponent<Move>().AblePlayer();
                Debug.Log("AblePLayer");
                parentName = null;
                Move.fUsed = false;
            }
            if(transform.parent.name == "Rock")
            {
                NPC.trade = false;
            }
            else if (NPC.trade && !reloadtrade )
            {
                transform.parent.GetComponent<NPCTrader>().TradeTrigger();
                reloadtrade = true;
            }
        }
    }
}
