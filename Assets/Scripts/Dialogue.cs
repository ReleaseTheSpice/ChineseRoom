using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour, ISelectableBehaviour
{
    public List<string> dialogue;

    [SerializeField]
    private TextMeshProUGUI textBox;

    private bool talking = false;

    private bool began = false;

    public void clicked(){
        if(!talking){
            talking = true;
            if(!began) StartCoroutine(RunLines());
        }
        
    }

    private IEnumerator RunLines()
    {
        began = true;
        float waitTime = 1f / 20f; // 75 letters per second (time to wait between characters)
        int index = 0;
        while(index < dialogue.Count){
            string line = dialogue[index];
            string dialogueText = "";
            foreach (char letter in line)
            {
                dialogueText += letter;
                textBox.SetText(dialogueText);
                yield return new WaitForSecondsRealtime(waitTime);
                
            }
            talking = false;
            yield return new WaitUntil(() => talking);
            index++;
        }
        talking = true;
        Destroy(this);
        yield return null;
    }
}
