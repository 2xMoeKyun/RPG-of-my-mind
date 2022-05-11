using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private Animator anim;
    public List<Transform> pointsToGo;
    private int currentIndex = 0;
    public float speed;
    public static bool setSpecialAnimation;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    //
    public static bool firstLevel = true;
    //
    //Dialogue
    public static bool npcDialogue;
    private int getDialogue;
    public bool EndOfCountDialogue;
    public bool isDialogue;
    //Trade
    public bool CanTrade = true;
    public bool trade;


    private void Update()
    {


        #region Dialogue
        if (npcDialogue && !DialogueManager.DialogueEnd && isDialogue )
        {
            if (transform.GetChild(0).GetChildCount() == getDialogue)
            {
                getDialogue = 0;
                EndOfCountDialogue = true;
                npcDialogue = false;
                isDialogue = false;
                if (transform.name != "Rock")
                {
                    StartCoroutine(ReloadDialogue());
                }
                if (CanTrade)
                {
                    trade = true;
                }
            }

            else
            {
                
                Debug.Log("Getdial" + getDialogue);
                transform.GetChild(0).GetChild(getDialogue).GetComponent<DialogueTrigger>().TriggerDialogue();
                getDialogue++;

                if (transform.tag == "Rock" && firstLevel)
                {
                    DialogueManager.SwitchesCount++;
                }
                if (DialogueManager.SwitchesCount == 1)
                {
                    DialogueManager.SwitchTo = "Player";
                    Debug.Log("work");
                }
                else if (DialogueManager.SwitchesCount == 3)
                {
                    DialogueManager.SwitchTo = "Rock";
                }

            }
        }
        #endregion
        if (setSpecialAnimation)
        {
            anim.SetBool("Walk", false);
            SpecialAnimation();
        }
        if (!stopFollow && pointsToGo[0] != null)
        {
            EndOfCountDialogue = true;
            Follow();
        }
    }


    private IEnumerator ReloadDialogue()
    {
        yield return new WaitForSeconds(0.2f);
        EndOfCountDialogue = false;
    }



    private void SpecialAnimation()
    {
        anim.SetTrigger("SpecialMove");
        setSpecialAnimation = false;
    }


    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        stopFollow = false;
    }

    private bool stopFollow;
    private void SwitchPoint()
    {
        anim.SetBool("Walk", false);
        stopFollow = true;
        if (currentIndex + 1 < pointsToGo.Count)
        {
            currentIndex++;
            StartCoroutine(Wait());
        }
        else
        {
            EndOfCountDialogue = false;
            return;
        }
    }

    private void Follow()
    {
        anim.SetBool("Walk", true);
        if (transform.position.x < pointsToGo[currentIndex].position.x) // vpravo
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Vector2.Distance(transform.position, pointsToGo[currentIndex].position) < 0.3f)
        {
            
            SwitchPoint();
        }

    }



 
}
