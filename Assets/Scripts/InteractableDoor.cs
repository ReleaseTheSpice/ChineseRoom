using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : MonoBehaviour, ISelectableBehaviour
{
    [SerializeField]
    private Transform doorLeft;
    [SerializeField]
    private Transform doorRight;

    private Vector3 altDoorPosLeft;
    private Vector3 altDoorPosRight;

    private bool open = false;

    private float elapsed = 0f;

    public void clicked()
    {
        open = !open;
        if(open){
            altDoorPosLeft = doorLeft.position + new Vector3(0,0,-1);
            altDoorPosRight = doorRight.position + new Vector3(0,0,1);
            elapsed = 1.0f;
            AudioManager.instance.PlaySound("door_close");
        } else {
            altDoorPosLeft = doorLeft.position + new Vector3(0,0,1);
            altDoorPosRight = doorRight.position + new Vector3(0,0,-1);
            elapsed = 1.0f;
            AudioManager.instance.PlaySound("door_open");
        }
    }

    public void Update(){
        if(elapsed > 0){
            doorLeft.position = Vector3.Lerp(altDoorPosLeft, doorLeft.position, elapsed);
            doorRight.position = Vector3.Lerp(altDoorPosRight, doorRight.position, elapsed);

            elapsed -= 0.001f;
        }
    }
}
