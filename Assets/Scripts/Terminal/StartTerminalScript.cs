using UnityEngine;

public class StartTerminalScript : MonoBehaviour
{
    public GameObject TerminalUI;
    public CharacterControl characterControl;

    bool touchingTerminal = false;
    private void Start()
    {
        TerminalUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && touchingTerminal)
        {
            GameManager.Instance.ToggleCursor();

            if (TerminalUI.activeInHierarchy)
            {
                Debug.Log("Close Terminal");
                TerminalUI.SetActive(false);
                characterControl.canMove = true;
            }
            else
            {
                AudioManager.instance.PlaySound("beep");
                Debug.Log("Open Terminal");
                TerminalUI.SetActive(true);
                characterControl.canMove = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            touchingTerminal = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            touchingTerminal = false;
        }
    }
}
