using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public enum Sender
{
    Player,
    Other
}

public class Message
{
    public Sender sender; // who sends the message
    public string message; // the message string
    public List<int> emojis; // list of emoji ids (0-72)

    public Message(Sender _sender, string _message, List<int> _emojis)
    {
        sender = _sender;
        message = _message;
        emojis = _emojis;
    }
}

public class Conversation
{
    public List<Message> messages;
    public int currentIndex;
    public bool hasEnded = false;

    public Message GetCurrentMessage()
    {
        return messages[currentIndex];
    }

    public void Reset()
    {
        currentIndex = 0;
        hasEnded = false;
    }

    public void IncrementMessage()
    {
        if (hasEnded)
            return;

        if (currentIndex == messages.Count-1)
        {
            hasEnded = true;
            Debug.Log("Conversation completed");
            return;
        }

        currentIndex++;

    }

    public Conversation()
    {
        currentIndex = 0;
        messages = new List<Message>();
    }
}

public class ConversationManager : MonoBehaviour
{
    public TextMeshProUGUI conversationTextBox;
    public TextMeshProUGUI emojiInputBox;
    public TextMeshProUGUI dictionaryText;
    public ScrollRect conversationScroller;

    private List<Conversation> conversations = new List<Conversation>();
    private Conversation currentConversation;

    private bool isGameOver = false;

    void Start()
    {
        InitializeConversations();
        currentConversation = conversations[Random.Range(0, conversations.Count - 1)]; // set current conversation
        conversationTextBox.text = "";
        dictionaryText.text = "";

        DisplayCurrentMessage(IsInEmojis: true);
    }

    private void Update()
    {
        // TESTING
        if (Input.GetKeyDown(KeyCode.Space))
        {
            emojiInputBox.text = ">" +
                EmojiIDsToTMPString(currentConversation.messages[currentConversation.currentIndex].emojis);
        }
    }

    // creates a string that works for textmeshpro from a list of emoji ids
    // strings should be in format: <sprite index=0><sprite index=0><sprite index=0><sprite index=0><sprite index=0>
    private string EmojiIDsToTMPString(List<int> emojiIds)
    {
        string results = "";
        for (int i = 0; i < emojiIds.Count; i++)
        {
            results += IntToEmojiTMPString(emojiIds[i]);
        }
        return results;
    }

    // convert single int into format: <sprite index=_num_>
    private string IntToEmojiTMPString(int num)
    {
        string results = "";
        results += "<sprite index=";
        results += num;
        results += ">";
        return results;
    }

    // creates a list of emoji ids from a string that works for textmeshpro
    private List<int> TMPStringToEmojiID(string str)
    {
        
        List<int> emojis = new List<int>();
        string str2 = Regex.Replace(str, "[^0-9]", " ");

        string curr = "";
        for (int i = 0; i < str2.Length; i++)
        {
            if (char.IsDigit(str2[i]))
            {
                curr += str2[i];
            }
            else
            {
                if (!curr.Equals(""))
                {
                    emojis.Add(int.Parse(curr));
                    curr = "";
                }
            }
        }

        return emojis;
    }

    // check if an emoji string matches the current message of the current conversation
    private bool ValidateCurrentMessage(string TMPstring)
    {
        return TMPstring == ">" + EmojiIDsToTMPString(currentConversation.messages[currentConversation.currentIndex].emojis);
    }

    // validates input and sends message
    public void SendMessage()
    {
        if (isGameOver)
            return;

        if (currentConversation.hasEnded)
        {
            TranslateCurrentConversation();
            isGameOver = true;
            return;
        }

        if (ValidateCurrentMessage(emojiInputBox.text))
        {
            emojiInputBox.text = ">";
            DisplayCurrentMessage(IsInEmojis: true);
            AudioManager.instance.PlaySound("accept");
        }
        else
        {
            AudioManager.instance.PlaySound("error");
        }
    }

    public void TranslateCurrentConversation()
    {
        conversationTextBox.text = "english translation\n\n";
        currentConversation.Reset();
        foreach (Message m in currentConversation.messages)
        {
            DisplayCurrentMessage(IsInEmojis: false, IsInPlayMode: false);
        }
        dictionaryText.text = "<sprite index=0>";
    }

    // display the current message into the message box
    public void DisplayCurrentMessage(bool IsInEmojis = false, bool IsInPlayMode = true)
    {
        if (currentConversation.hasEnded)
        {
            return;
        }

        if (currentConversation.GetCurrentMessage().sender == Sender.Player)
        {
            conversationTextBox.text += "You: ";
        }
        else
        {
            conversationTextBox.text += "Bob: ";
        }

        if (IsInEmojis)
        {
            conversationTextBox.text += EmojiIDsToTMPString(currentConversation.GetCurrentMessage().emojis);
        }
        else
        {
            conversationTextBox.text += currentConversation.GetCurrentMessage().message;
        }
        conversationTextBox.text += "\n\n";

        if (!IsInPlayMode)
        {
            currentConversation.IncrementMessage();
            return;
        }
        conversationScroller.velocity = new Vector2(0, 1000f);

        if (currentConversation.GetCurrentMessage().sender == Sender.Player)
        {
            currentConversation.IncrementMessage();
            DisplayCurrentMessage(IsInEmojis: IsInEmojis);
        }
        else
        {
            DisplayDictionaryPage();
        }
        currentConversation.IncrementMessage();
        if (currentConversation.hasEnded)
        {
            dictionaryText.text = "Conversation Complete\nPress Enter";
        }
    }

    // display the current message into the dictionary page
    public void DisplayDictionaryPage()
    {
        
        if (currentConversation.currentIndex < currentConversation.messages.Count-2)
        {
            dictionaryText.text = "IF: ";
            dictionaryText.text += EmojiIDsToTMPString(currentConversation.GetCurrentMessage().emojis);
            dictionaryText.text += "\n\nTHEN: ";
            dictionaryText.text += EmojiIDsToTMPString(currentConversation.messages[currentConversation.currentIndex+1].emojis);
        }

    }

    private void InitializeConversations()
    {
        // Team Rocket
        Conversation TeamRocket = new();
        TeamRocket.messages.Add(new Message(Sender.Other, "Prepare for trouble!", new List<int> { 63, 65}));
        TeamRocket.messages.Add(new Message(Sender.Player, "And make it double!", new List<int> { 63, 65, 65 }));
        TeamRocket.messages.Add(new Message(Sender.Other, "To protect the world from devastation!", new List<int> { 5, 64, 30}));
        TeamRocket.messages.Add(new Message(Sender.Player, "To unite all peoples within our nation!", new List<int> { 17, 7 }));
        TeamRocket.messages.Add(new Message(Sender.Other, "To denounce the evils of truth and love!", new List<int> { 25, 5, 22}));
        TeamRocket.messages.Add(new Message(Sender.Player, "To extend our reach to the stars above!", new List<int> { 51, 48, 48}));
        TeamRocket.messages.Add(new Message(Sender.Other, "Jessie!", new List<int> { 51, 49 }));
        TeamRocket.messages.Add(new Message(Sender.Player, "James!", new List<int> { 51, 43 }));
        TeamRocket.messages.Add(new Message(Sender.Other, "Team Rocket blasts off at the speed of light!", new List<int> { 36, 6, 5, 3}));
        TeamRocket.messages.Add(new Message(Sender.Player, "Surrender now, or prepare to fight!", new List<int> { 3, 2, 1 }));
        TeamRocket.messages.Add(new Message(Sender.Other, "Meowth! That's right!", new List<int> { 60, 72 }));
        conversations.Add(TeamRocket);

        // Calvin and Hobbes
        Conversation CalvinHobbes = new();
        CalvinHobbes.messages.Add(new Message(Sender.Other, "Do you have an idea for your project yet? ", new List<int> { 4, 18, 52 }));
        CalvinHobbes.messages.Add(new Message(Sender.Player, "No, I'm waiting for inspiration, " +
                                                             "you can't just turn on creativity " +
                                                             "like a faucet, you have to be in the right mood. ", new List<int> { 56, 4, 66, 22, 69, 14}));
        CalvinHobbes.messages.Add(new Message(Sender.Other, "What mood is that?", new List<int> { 66, 22, 1 }));
        CalvinHobbes.messages.Add(new Message(Sender.Player, "Last-minute panic.", new List<int> { 30, 18, 57 }));
        conversations.Add(CalvinHobbes);

        // Bomb Cart
        Conversation BombCart = new();
        BombCart.messages.Add(new Message(Sender.Other, "Okay we have to get this bomb out of the room. We have 1 minute to do so. ", new List<int> { 55, 51, 30, 30, 10}));
        BombCart.messages.Add(new Message(Sender.Player, "It’s attached to a cart, so if we move the cart the bomb should follow. ", new List<int> { 55, 19, 34, 4}));
        BombCart.messages.Add(new Message(Sender.Other, "Alright, but if we pull the cart would the colour of the walls change to green?", new List<int> { 44, 55, 11}));
        BombCart.messages.Add(new Message(Sender.Player, "No, but would they change to blue? ", new List<int> { 71, 65, 66}));
        BombCart.messages.Add(new Message(Sender.Other, "No, but would they change to yellow?", new List<int> { 71, 65, 62}));
        BombCart.messages.Add(new Message(Sender.Player, "No, but...", new List<int> { 71, 65 }));
        conversations.Add(BombCart);

        // Bat
        Conversation Bat = new();
        Bat.messages.Add(new Message(Sender.Other, "Have you ever wondered what it’s like to be a bat?", new List<int> { 5, 72, 37, 14 }));
        Bat.messages.Add(new Message(Sender.Player, "No, but I imagine it must be quite thrilling, hardly seeing and all.", new List<int> { 26, 13, 15, 12, 67, 67}));
        Bat.messages.Add(new Message(Sender.Other, "But you could never know what it was like to be a bat, with its echolocation and wings.", new List<int> { 22, 33, 6, 0, 14 }));
        Bat.messages.Add(new Message(Sender.Player, "Yeah, but if I could obtain echolocation and wings, then I would know what it would be like to be a bat.", new List<int> { 21, 0, 33, 6, 14 }));
        Bat.messages.Add(new Message(Sender.Other, "No, you would know what it was like to have echolocation and wings, but not what it’s like to be a bat.", new List<int> { 14, 33, 6, 11, 11 }));
        Bat.messages.Add(new Message(Sender.Player, "Ok, then what if I made myself half-bat, then I should surely know what it’s like to be a bat.", new List<int> { 14, 12, 33, 22, 58, 68 }));
        Bat.messages.Add(new Message(Sender.Other, "But then you would know what it is like to be half-bat, half human, not what its like to be a bat.", new List<int> { 14, 12, 5, 72 }));
        Bat.messages.Add(new Message(Sender.Player, "So how will I know what it’s like to be a bat?", new List<int> { 37, 14, 1, 24 }));
        Bat.messages.Add(new Message(Sender.Other, "Be a bat.", new List<int> { 37, 14 }));
        conversations.Add(Bat);

        // It is what it is
        Conversation WhatIs = new();
        WhatIs.messages.Add(new Message(Sender.Other, "And your final question is: What is the full version of this phrase? \"It is (blank) it is.\"", new List<int> { 1, 1, 5, 43, 44, 22, 21}));
        WhatIs.messages.Add(new Message(Sender.Player, "What is \"it is what it is?\"", new List<int> { 43, 44, 21, 1, 19}));
        WhatIs.messages.Add(new Message(Sender.Other, "Is \"what is, it is what it is?\" your final answer?", new List<int> { 43, 44, 21, 50, 49}));
        WhatIs.messages.Add(new Message(Sender.Player, "It is", new List<int> { 43 }));
        WhatIs.messages.Add(new Message(Sender.Other, "\"What is, it is what it is\" is not the correct answer, the correct answer is \"what is, it is how it is\".", new List<int> { 43, 44, 21, 47, 31}));
        WhatIs.messages.Add(new Message(Sender.Player, "It is what it is.", new List<int> { 43, 44, 21 }));
        WhatIs.messages.Add(new Message(Sender.Other, "Actually it isn't.", new List<int> { 48, 43, 39 }));
        conversations.Add(WhatIs);
    }
}
