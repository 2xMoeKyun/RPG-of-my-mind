using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogueUI;
    private DialogueManager dm;
    public Dialogue dialogue;

    private void Start()
    {
        dm = GetComponent<DialogueManager>();
    }

    public void TriggerDialogue()
    {
        dialogueUI.SetActive(true);
        dm.StartDialogue(dialogue);
    }
}
