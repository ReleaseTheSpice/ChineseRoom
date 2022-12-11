using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueRunner : MonoBehaviour
{
    public List<DialogueUI.DialogueLine> dialogue = new List<DialogueUI.DialogueLine>();
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("dialoguebox").GetComponent<DialogueUI>().RunDialogue(dialogue);
    }
}
