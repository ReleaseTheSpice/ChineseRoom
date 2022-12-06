using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public Canvas promptCanvas;

    private Transform selectedObject;

    private int selectedId = -1;
    private int newSelectedId = 0;

    public bool selectedAgain()
    {
        return selectedId == newSelectedId;
    }

    void Update()
    {
        if (selectedObject != null)
        {
            selectedObject = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.GetComponent<ISelectableBehaviour>() != null
                && Vector3.Distance(hit.transform.position, transform.position) <= 5)
            {
                selectedObject = hit.transform;
                if(!promptCanvas.gameObject.activeSelf) promptCanvas.gameObject.SetActive(true);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (selectedObject != null && selectedObject.GetComponent<ISelectableBehaviour>() != null)
            {
                selectedObject.GetComponent<ISelectableBehaviour>().clicked();
            }
        }

        if (selectedObject == null && promptCanvas.gameObject.activeSelf)
        {
            promptCanvas.gameObject.SetActive(false);
        }
    }

    
}
