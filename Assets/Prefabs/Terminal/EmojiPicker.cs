using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiPicker : MonoBehaviour
{
    public GameObject EmojiButton;
    public GameObject Content;

    void Start()
    {
        int count = 0;
        // populate emoji picker
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 25; i++)
            {
                if (count > 72)
                    break;

                GameObject emoji = Instantiate(EmojiButton, new Vector3(0, 0, 0), Quaternion.identity);
                emoji.transform.SetParent(Content.transform, false);
                emoji.GetComponent<ClickableEmoji>().ChangeEmoji(count);
                count++;
            }
        }
    }
}
