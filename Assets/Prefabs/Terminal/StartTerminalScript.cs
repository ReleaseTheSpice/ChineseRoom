using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTerminalScript : MonoBehaviour
{
    bool touchingTerminal = false;
    private void Start()
    {

    }

    private void Update()
    {
        if (touchingTerminal)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Start Terminal");
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
