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
    [SerializeField] float turnAnimationSmoothLerpFactor = 10f;
    [SerializeField] CameraRig cameraRig;
    CharacterController characterController;
    InventoryComponent inventoryComponent;
    Vector2 moveInput;
    Vector2 aimInput;

    Vector3 moveDir;
    Vector3 aimDir;

    Camera myCamera;

    Animator animator;

    float animTurnSpeed = 0f;

    public void SwitchWeapon()
    {
        inventoryComponent.NextWeapon();

    }

    private void Awake() // for initiallizing values
    {
        moveStick.onInputValueChanged += MoveInputUpdated;
        aimStick.onInputValueChanged += AimInputUpdated;
        aimStick.onStickTapped += AimStickTapped;
        characterController = GetComponent<CharacterController>();
        myCamera = Camera.main;
        animator = GetComponent<Animator>();
        inventoryComponent = GetComponent<InventoryComponent>();
    }

    private void AimStickTapped()
    {
        animator.SetTrigger("switchWeapon");
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
        float rightSpeed = Vector3.Dot(moveDir, transform.right);  // dot product is cheaper than cross product, use it more often
        float forwardSpeed = Vector3.Dot(moveDir, transform.forward);

        animator.SetFloat("leftSpeed", -rightSpeed);
        animator.SetFloat("forwardSpeed", forwardSpeed);

        animator.SetBool("firing", aimInput.magnitude > 0);

    }

    private void ProcessAimInput()
    {
        // if aim has input, use the aim to determine the turning, if not, use the move input.
        Vector3 lookDir = aimDir.magnitude != 0 ? aimDir : moveDir; // oneliner considered bad practice

        float goalAnimTurnSpeed = 0f;
        if (lookDir.magnitude != 0)
        {
            Quaternion prevRot = transform.rotation; // before rotate

            Quaternion nextRot = Quaternion.LookRotation(lookDir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, nextRot, turnSpeed * Time.deltaTime);
            Quaternion newRot = transform.rotation; // after rotate

            float rotationDelta = Quaternion.Angle(prevRot, newRot); // how much we have rotated in this frame.

            float rotateDir = Vector3.Dot(lookDir, transform.right) > 0 ? 1 : -1;

            goalAnimTurnSpeed = rotationDelta / Time.deltaTime * rotateDir;
        }

        // Smoothes out the turning
        animTurnSpeed = Mathf.Lerp(animTurnSpeed, goalAnimTurnSpeed, turnSpeed * Time.deltaTime * turnAnimationSmoothLerpFactor);
        if(animTurnSpeed < 0.01f)
        {
            animTurnSpeed = 0f;
        }

        animator.SetFloat("turnSpeed", animTurnSpeed);
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

    public void DamagePoint()
    {
        Debug.Log("damage point");
        inventoryComponent.DamagePoint();
    }
}
