using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSelectableBehaviour : MonoBehaviour, ISelectableBehaviour
{
    public List<DialogueUI.DialogueLine> dialogue0 = new List<DialogueUI.DialogueLine>();
    public void clicked()
    {
        GameObject.Find("dialoguebox").GetComponent<DialogueUI>().RunDialogue(dialogue0);
    }
}
