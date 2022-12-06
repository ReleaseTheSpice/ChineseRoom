using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    public TextMeshProUGUI speaker;
    public TextMeshProUGUI text;

    public Animator dialogAnim;

    // When true, moves to next dialogue line in queue
    private bool moveNext = false;

    // Holds the dialogue lines to run next
    private List<DialogueLine> lineQueue = new List<DialogueLine>();

    //Event to detect when dialog is over
    public delegate void DialogEventEnd();
    public event DialogEventEnd onDialogueEnd;

    // Checks if dialogue is currently running
    public static bool dialogueRunning = false;

    // Is text currently scrolling
    private bool textScrolling = false;

    public static bool canClick = true;

    // Dialogue print speed
    public int lettersPerSecond = 75;

    // Holds a single piece of dialogue
    [System.Serializable]
    public class DialogueLine {

        [SerializeField] // Speaker's name
        public string speaker;
        [SerializeField] // Dialogue text
        public string text;

        [SerializeField]
        public delegate void DialogueEvent();
        [SerializeField] // Event that runs on dialogue line
        public event DialogueEvent onDialogue;

        [SerializeField] // Determines if event runs on the start or end of dialogue line
        public bool onDialogueStart;

        public DialogueLine(string speaker, string text, DialogueEvent onDialogue = null, bool onDialogueStart = true)
        {
            this.speaker = speaker;
            this.text = text;
            this.onDialogue = onDialogue;
        }

        public void runDialogueEvent()
        {
            if (onDialogue != null)
            {
                onDialogue();
            }
        }
        
    }

    void Awake()
    {
        showDialogueBox(false);
        dialogAnim = GetComponent<Animator>();
    }

    // Start a dialogue
    public void RunDialogue(List<DialogueLine> script)
    {
        lineQueue.RemoveRange(0, lineQueue.Count);
        foreach (DialogueLine line in script)
        {
            lineQueue.Add(line);
        }
        StartCoroutine(RunLines());
    }

    // Run all the lines in a list of DialogueLine objects
    private IEnumerator RunLines()
    {
        canClick = false;
        showDialogueBox(true);
        dialogAnim.SetTrigger("triggerOpen");
        speaker.text = "";
        text.text = "";
        yield return new WaitForSeconds(dialogAnim.GetCurrentAnimatorStateInfo(0).length - 0.1f);

        for(int i = 0; i < lineQueue.Count; i++)
        {
            moveNext = false;
            // Run delegate onStart
            if (lineQueue[i].onDialogueStart) { 
                lineQueue[i].runDialogueEvent(); 
            }
            
            yield return StartCoroutine(RunLine(lineQueue[i].speaker, lineQueue[i].text));

            // Run delegate !onStart
            if (!lineQueue[i].onDialogueStart) { 
                lineQueue[i].runDialogueEvent(); 
            }
        }

        if (onDialogueEnd != null)
        {
            onDialogueEnd();
        }

        
        dialogAnim.ResetTrigger("triggerOpen");
        dialogAnim.SetTrigger("triggerClose");
        dialogueRunning = false;
        yield return new WaitForSeconds(dialogAnim.GetCurrentAnimatorStateInfo(0).length);
        showDialogueBox(false);
        canClick = true;
        StopAllCoroutines();
    }

    public void logText(string speaker, string text)
    {
        if (GameObject.Find("chatlog"))
        {
            TextMeshProUGUI chatlog = GameObject.Find("chatlog").GetComponent<TextMeshProUGUI>();
            chatlog.pageToDisplay = chatlog.textInfo.pageCount;
            chatlog.text += "[" + speaker + "] " + text + "\n";
            if (chatlog.textInfo.lineCount >= (6*chatlog.textInfo.pageCount) && chatlog.textInfo.pageCount > 0)
            {
                chatlog.pageToDisplay++;
            }
        }
    }

    // Runs a single line through the dialogue box
    private IEnumerator RunLine(string sp, string line)
    {
        
        string dialogueText = "";
        float waitTime = 1f / lettersPerSecond; // time to wait between characters
        
        speaker.text = sp;
        textScrolling = true;

        foreach (char letter in line)
        {
            if (textScrolling) // adds characters to text box
            {
                dialogueText += letter;
                text.text = dialogueText;
                yield return new WaitForSecondsRealtime(waitTime);
            } else // cancels the scroll
            {
                text.text = line;
                break;
            }
            
        }
        logText(sp, line);
        textScrolling = false;
        
        yield return new WaitUntil(() => moveNext);
        
    }

    // Hides and unhides the dialogue box
    private void showDialogueBox(bool show)
    {
        if (show)
        {
            transform.localScale = new Vector3(1, 1, 1);
            dialogueRunning = true;
        } else
        {
            dialogueRunning = false;
            transform.localScale = new Vector3(0, 0, 0);
        }
    }
    private void Update()
    {
        // Space key progresses dialogue
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (textScrolling)
            {
                // If space is pressed during a text scroll it will skip the scroll instead
                textScrolling = false;
            } else
            {
                moveNext = true;
            }
        }

        // text scroll
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            // scroll previous
            if (GameObject.Find("chatlog"))
            {
                TextMeshProUGUI chatlog = GameObject.Find("chatlog").GetComponent<TextMeshProUGUI>();
                chatlog.pageToDisplay--;
            }

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            // scroll next
            if (GameObject.Find("chatlog"))
            {
                TextMeshProUGUI chatlog = GameObject.Find("chatlog").GetComponent<TextMeshProUGUI>();
                if (chatlog.pageToDisplay < chatlog.textInfo.pageCount)
                    chatlog.pageToDisplay++;
            }
        }
    }
}
