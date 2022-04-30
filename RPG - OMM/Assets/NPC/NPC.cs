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

    public static bool npcDialogue;
    public static int getDialogue;
    public static bool EndOfCountDialogue;
    private void Update()
    {
        if (npcDialogue && !DialogueManager.DialogueEnd)
        {
            if (transform.GetChild(0).GetChildCount() == getDialogue)
            {
                Debug.Log("enddialogue");
                getDialogue = 0;
                EndOfCountDialogue = true;
                npcDialogue = false;
            }
            else
            {
                transform.GetChild(0).GetChild(getDialogue).GetComponent<DialogueTrigger>().TriggerDialogue();
                getDialogue++;
                if(transform.tag == "Rock")
                {
                    DialogueManager.SwitchesCount++;
                }
                
                if (DialogueManager.SwitchesCount == 1 || DialogueManager.SwitchesCount == 3 )
                {
                    DialogueManager.SwitchTo = "Player";
                    Debug.Log("work");
                }
            }
        }
        if (setSpecialAnimation)
        {
            anim.SetBool("Walk", false);
            SpecialAnimation();
        }
        if (!stopFollow && pointsToGo[0] != null)
        {
            Follow();
        }
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
            return;
        }
    }

    private void Follow()
    {
        anim.SetBool("Walk", true);
        if (transform.position.x < pointsToGo[currentIndex].position.x) // vpravo
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }
        if (Vector2.Distance(transform.position, pointsToGo[currentIndex].position) < 0.3f)
        {
            
            SwitchPoint();
        }

    }



 
}
