using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 gravity = Vector3.zero;
    private float runningSpeed = 5f;
    public bool canMove = true;
    public float speed = 5f;
    public float rotationSpeed = 180f;

    private bool isMoving = false;

    private Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        StartCoroutine(StepLoop());
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
            return;

        if (characterController.isGrounded && Input.GetKey(KeyCode.LeftShift))
        {
            runningSpeed = speed * 2;
        }
        else
        {
            runningSpeed = speed;
        }

        if (!characterController.isGrounded)
        {
            gravity += Physics.gravity;
        }
        else
        {
            gravity = Vector3.zero;
        }

        this.rotation = new Vector3(0, Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime, 0);

        Vector3 move = new Vector3(0, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime) + gravity;

        isMoving = (Input.GetAxisRaw("Vertical") > 0);
        if (Input.GetKeyDown(KeyCode.W) && isMoving)
        {
            AudioManager.instance.PlaySound("step");
        }

        move = this.transform.TransformDirection(move);
        characterController.Move(move * runningSpeed);
        this.transform.Rotate(this.rotation);
    }

    public IEnumerator StepLoop()
    {
        if (canMove && isMoving)
            AudioManager.instance.PlaySound("step");
        yield return new WaitForSeconds(.5f);
        StartCoroutine(StepLoop());
    }
}
