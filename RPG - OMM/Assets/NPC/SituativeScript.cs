using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SituativeScript : MonoBehaviour // this sript must be component of Rock(NPC)'s child(DTrigger)
{
    public GameObject point;
    public GameObject deleteDialogue;
    public GameObject parentThisDialogue;

    private bool srabotalo = true;
    private bool npcGo;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Move.SetAblePlayer && npcGo )
        {
            transform.parent.GetComponent<NPC>().pointsToGo[0] = point.transform;
            transform.parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            transform.parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            parentThisDialogue.transform.SetParent(transform.parent.GetChild(0).transform);
            Destroy(deleteDialogue.gameObject);
            Destroy(this);
        }
        if (srabotalo /*&& ManagerScene.SceneSwitch*/)//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        {
            transform.parent.GetComponent<NPC>().CanTrade = false;
            NPC.firstLevel = false;
            point.SetActive(true);
            Move.fUsed = true;
            srabotalo = false;
            TransitionManager.SceneSwitch = false;
            StartCoroutine(NPCGO());
        }

    }

    private IEnumerator NPCGO()
    {
        yield return new WaitForSeconds(0.4f);
        npcGo = true;
    }

}
