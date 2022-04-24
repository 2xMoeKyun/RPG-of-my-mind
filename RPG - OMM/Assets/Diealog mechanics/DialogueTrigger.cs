using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dm;
    public Dialogue dialogue;
    public void TriggerDialogue()
    {
        dm.dialogueUI.SetActive(true);
        dm.StartDialogue(dialogue);
    }
}
