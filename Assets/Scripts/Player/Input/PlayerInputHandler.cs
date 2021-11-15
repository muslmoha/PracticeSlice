using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;

    public int NormInputY { get; private set; }

    private Vector2 rawJumpInput;
    public Vector2 JumpDirectionInput { get; private set; }
    private int facingDirection;
    private Vector2 workSpace;
    public Vector2 CurrentVelocty;
    public Vector2 RawMovementInput { get; private set; }
    private Vector2 jumpToPosition;

    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (!JumpInput)
        {
            RawMovementInput = context.ReadValue<Vector2>();
            NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
            //SetVelocityY(NormInputY);
            //This just gets the input and normalizes it to 1 or -1, exclusively for a keyboard
            //This doesn't do anything with the input yet though
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started && !JumpInput) //While also not already jumping
        {
            rawJumpInput = Mouse.current.position.ReadValue();//Reads in mouse position for new input system
            rawJumpInput = cam.ScreenToWorldPoint(rawJumpInput) - transform.position;
            //Now it will be from player position to cursor location
            JumpDirectionInput = (rawJumpInput.normalized);
            JumpInput = true;
            //SetJumpVelocity(jumpDirectionInput);

        }
    }

    public void UseJumpInput() => JumpInput = false;
}
