using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, ITeamInterface
{
    [SerializeField] private Joystick moveStick;
    [SerializeField] private Joystick aimStick;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float turnSpeed = 10f;
    [SerializeField] float turnAnimationSmoothLerpFactor = 10f;
    [SerializeField] CameraRig cameraRig;
    [SerializeField] int teamID = 1;
    [SerializeField] UIManager uiManager;

    ////TODO: Testing only
    //[SerializeField] Shop test_shop;
    //[SerializeField] ShopItem test_Item;

    CharacterController characterController;
    InventoryComponent inventoryComponent;
    MovementComponent movementComponent;
    HealthComponent healthComponent;
    Vector2 moveInput;
    Vector2 aimInput;

    Vector3 moveDir;
    Vector3 aimDir;

    Camera myCamera;

    Animator animator;

    float animTurnSpeed = 0f;

    //public int GetTeamID()

    public void SwitchWeapon()
    {
        inventoryComponent.NextWeapon();

    }

    private void Awake() // for initiallizing values
    {
        GameplayStatics.SetPlayerGameObject(gameObject);

        moveStick.onInputValueChanged += MoveInputUpdated;
        aimStick.onInputValueChanged += AimInputUpdated;
        aimStick.onStickTapped += AimStickTapped;
        characterController = GetComponent<CharacterController>();
        myCamera = Camera.main;
        animator = GetComponent<Animator>();
        inventoryComponent = GetComponent<InventoryComponent>();
        movementComponent = GetComponent<MovementComponent>();
        healthComponent = GetComponent<HealthComponent>();
        healthComponent.onHealthEmpty += StartDeath;
    }

    private void StartDeath(float delta, float maxHealth)
    {
        animator.SetTrigger("die");
        uiManager.SetGameplayControlEnabled(false);
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
        //Invoke("Test_Purchase", 2f);
    }


    // Update is called once per frame
    void Update()
    {
        ProcessMoveInput();
        ProcessAimInput();
        UpdateAnimation();
    }

    //void Test_Purchase()
    //{
    //    test_shop.TryPurchase(test_Item, GetComponent<CreditComponent>());
    //}


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

        float goalAnimTurnSpeed = movementComponent.RotateTowards(lookDir);

        if (lookDir.magnitude != 0)
        {
            
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

    public void DamagePointPC()
    {
        Debug.Log("damage point");
        inventoryComponent.DamagePoint();
    }
}
