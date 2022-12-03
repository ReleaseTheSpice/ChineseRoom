using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTerminalScript : MonoBehaviour
{
    public GameObject TerminalUI;
    public CharacterControl characterControl;

    bool touchingTerminal = false;
    private void Start()
    {
        TerminalUI.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && touchingTerminal)
        {
            if (TerminalUI.activeInHierarchy)
            {
                Debug.Log("Close Terminal");
                TerminalUI.SetActive(false);
                characterControl.canMove = true;
            }
            else
            {
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
