using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour, ISelectableBehaviour
{
    public string dialogue;

    [SerializeField]
    private TextMeshProUGUI textBox;

    private bool talking = false;

    public void clicked(){
        if(!talking){
            talking = true;
            StartCoroutine(RunLine(dialogue));
        }
        
    }

    private IEnumerator RunLine(string line)
    {
        
        string dialogueText = "";
        float waitTime = 1f / 20f; // 75 letters per second (time to wait between characters)

        foreach (char letter in line)
        {
            dialogueText += letter;
            textBox.SetText(dialogueText);
            yield return new WaitForSecondsRealtime(waitTime);
            
        }
        //textBox.SetText("");
        talking = false;
    }
}
