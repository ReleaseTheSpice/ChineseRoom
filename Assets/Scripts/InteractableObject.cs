// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class InteractableObject : MonoBehaviour, ISelectableBehaviour
// {
//     public bool importantObject;
//     public List<DialogueUI.DialogueLine> objectDialogue = new List<DialogueUI.DialogueLine>();

//     public bool interactedWith = false;

//     public void clicked()
//     {
//         if (importantObject)
//         { 
//             LevelManager.endingCount++;
//             Debug.Log(LevelManager.endingCount);
//         }

//         GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = false;
//         GameObject.Find("Player").GetComponent<CameraMovement>().canLook = false;

//         GameObject.Find("dialoguebox").GetComponent<DialogueUI>().RunDialogue(objectDialogue);
       
//         GameObject.Find("dialoguebox").GetComponent<DialogueUI>().onDialogueEnd += () =>
//         {
//             GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
//             GameObject.Find("Player").GetComponent<CameraMovement>().canLook = true;
//             interactedWith = true;
//         };
//     }
// }
