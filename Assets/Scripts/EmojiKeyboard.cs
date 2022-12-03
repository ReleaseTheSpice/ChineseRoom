using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EmojiKeyboard : MonoBehaviour
{
    public ConversationManager conversationManager;

    private TMP_Text mtextComponent;
    private bool mkeyDown = false;

    private void Awake()
    {
        mtextComponent = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if(!mkeyDown)
        {
            if(Input.GetKeyDown(KeyCode.Backspace))
            {
                mtextComponent.text = mtextComponent.text.Substring(0, mtextComponent.text.Length - GetLastTagSize(mtextComponent.text));
                mkeyDown = true;
            }
            else if(Input.GetKeyDown(KeyCode.Return))
            {
                //EXTRACT INPUT VALUE HERE

                print(mtextComponent.text.Remove(0,1));
                conversationManager.SendMessage();

                mtextComponent.text = ">";
                mkeyDown = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Backspace) || Input.GetKeyUp(KeyCode.Return))
                mkeyDown = false;
    }

    int GetLastTagSize(string value)
    {
        if (value.Length <= 1)
            return 0;

        int lindex = value.Length - 1, size = 0;
        do
        {
            lindex--;
            size++;
        } while (value[lindex] != '<');

        return size + 1;
    }
}
