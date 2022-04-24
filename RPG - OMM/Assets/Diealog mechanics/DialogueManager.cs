using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;
    public GameObject dialogueUI;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        
        Move.SetAblePlayer = false;
        NPC.npcDialogue = false;
        Move.playerDialogue = false;
        DialogueEnd = true;
        nameText.text = dialogue.name; 

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentences();
    }

    public void DisplayNextSentences()
    {
        if(sentences.Count == 0)
        {
            
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public static string SwitchTo;
    public static int SwitchesCount;
    public static bool DialogueEnd;
    private void EndDialogue()
    {
        Move.SetAblePlayer = true;
        dialogueUI.SetActive(false);
        DialogueEnd = false;
        if(SwitchTo != "")
        {
            GameObject.FindGameObjectWithTag(SwitchTo).GetComponent<DialogueTrigger>().TriggerDialogue();
            SwitchTo = "";
        }
    }
}
