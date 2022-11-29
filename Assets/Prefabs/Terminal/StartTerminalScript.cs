using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTerminalScript : MonoBehaviour
{
    public GameObject TerminalUI;

    bool touchingTerminal = false;
    private void Start()
    {
        TerminalUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && touchingTerminal)
        {
            if (TerminalUI.activeInHierarchy)
            {
                Debug.Log("Close Terminal");
                TerminalUI.SetActive(false);
            }
            else
            {
                Debug.Log("Open Terminal");
                TerminalUI.SetActive(true);
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
