using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private Joystick moveStick;
    [SerializeField] private Joystick aimStick;
    [SerializeField] float moveSpeed = 5f;
    CharacterController characterController;

    private void Awake() // for initiallizing values
    {
        moveStick.onInputValueChanged += MoveInputUpdated;
        aimStick.onInputValueChanged += AimInputUpdated;

        characterController = GetComponent<CharacterController>();
    }

    private void AimInputUpdated(Vector2 inputVal)
    {

    }

    private void MoveInputUpdated(Vector2 inputVal)
    {
        characterController.Move(new Vector3(inputVal.x, 0f, inputVal.y) * Time.deltaTime * moveSpeed);
    }

    // Start is called before the first frame update
    void Start()
    {
        // starting logics
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
