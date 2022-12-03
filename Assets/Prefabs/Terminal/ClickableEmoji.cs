using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickableEmoji : MonoBehaviour
{
    [Range(0,72)]
    public int emojiId;
    public TextMeshProUGUI textbox;

    private ConversationManager cm;

    void Start()
    {
        cm = GameObject.Find("TerminalCanvas").GetComponent<ConversationManager>();
        textbox.text = "<sprite index=" + emojiId + ">";
    }

    public void ChangeEmoji(int id)
    {
        emojiId = id;
        textbox.text = "<sprite index=" + emojiId + ">";
    }

    public void SendToInput()
    {
        if (cm.emojiInputBox.text.Length + textbox.text.Length > 239)
            return;
        cm.emojiInputBox.text += textbox.text;
    }
}
