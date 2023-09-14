using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private Joystick moveStick;
    [SerializeField] private Joystick aimStick;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float turnSpeed = 10f;
    [SerializeField] CameraRig cameraRig;
    CharacterController characterController;
    Vector2 moveInput;
    Vector2 aimInput;

    Vector3 moveDir;
    Vector3 aimDir;

    Camera myCamera;

    Animator animator;

    private void Awake() // for initiallizing values
    {
        moveStick.onInputValueChanged += MoveInputUpdated;
        aimStick.onInputValueChanged += AimInputUpdated;

        characterController = GetComponent<CharacterController>();
        myCamera = Camera.main;
        animator = GetComponent<Animator>();
    }

    private void AimInputUpdated(Vector2 inputVal)
    {
        aimInput = inputVal;
        aimDir = ConvertInputToWorldDirection(aimInput);
    }

    private void MoveInputUpdated(Vector2 inputVal)
    {
        moveInput = inputVal;
        moveDir = ConvertInputToWorldDirection(moveInput);

    }

    // Start is called before the first frame update
    void Start()
    {
        // starting logics
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMoveInput();
        ProcessAimInput();
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        float leftSpeed = Vector3.Dot(moveDir, transform.right);  // dot product is cheaper than cross product, use it more often
        float forwardSpeed = Vector3.Dot(moveDir, transform.forward);

        animator.SetFloat("leftSpeed", leftSpeed);
        animator.SetFloat("forwardSpeed", forwardSpeed);

    }

    private void ProcessAimInput()
    {
        // if aim has input, use the aim to determine the turning, if not, use the move input.
        Vector3 lookDir = aimDir.magnitude != 0 ? aimDir : moveDir; // oneliner considered bad practice

        if (lookDir.magnitude != 0)
        {
            //transform.rotation = 
            Quaternion nextRot = Quaternion.LookRotation(lookDir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, nextRot, turnSpeed * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        UpdateCamera();
        
    }

    private void UpdateCamera()
    {
        if(aimDir.magnitude == 0)
        {
            cameraRig.AddYawInput(moveInput.x);
        }

    }

    Vector3 ConvertInputToWorldDirection(Vector2 inputVal)
    {
        Vector3 rightDir = myCamera.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up); // cross is more expensive

        return (rightDir * inputVal.x + upDir * inputVal.y).normalized;
    }

    private void ProcessMoveInput()
    {
        characterController.Move(moveDir * moveSpeed * Time.deltaTime);

    }
}
