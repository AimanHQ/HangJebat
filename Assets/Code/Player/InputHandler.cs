using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HQ 
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;
        public bool b_input;
        public bool G_input;
        public bool rb_input;
        public bool rt_input;
        public bool rs;
        public bool RollFlag;
        public bool sprintFlag;
        public float rollInputTimer;
        PlayerControl inputAction;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        CameraHandler cameraHandler;
        AnimatorHandler animatorHandler;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Awake()
        {
            playerAttacker =  GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }
        public void OnEnable()
        {
            if(inputAction == null) {
                inputAction = new PlayerControl();
                inputAction.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputAction.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            }

            inputAction.Enable();
        }

        private void OnDisable()
        {
            inputAction.Disable();
        }

        public void TickInput(float delta)
        {
            moveInput(delta);
            HandleRollInput(delta);
            HandleAttackInput(delta);
            HandleQuickSlotInput();
            HandleInteractableButtonInput();
        }

        private void moveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }
    
        private void HandleRollInput(float delta)
        {
            b_input = inputAction.PlayerActions.Roll.inProgress;

            if(b_input) {
                rollInputTimer += delta;
                sprintFlag = true;
            }
            else {
                if (rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    sprintFlag = false;
                    RollFlag = true;
                }

                rollInputTimer = 0;
            }
        }
    
        private void HandleAttackInput(float delta)
        {
            inputAction.PlayerActions.RB.performed += i => rb_input = true;
            inputAction.PlayerActions.RT.performed += i => rt_input = true;

            //RB input handle right hand weapon light attack
            if(rb_input) {
                animatorHandler.anim.SetBool("isUsingRightHand", true);
                playerAttacker.HandleLightAttack(playerInventory.rightweapon);
            }

            if (rt_input) {
                playerAttacker.HandleHeavyAttack(playerInventory.rightweapon);
            }
        }

        private void HandleQuickSlotInput()
        {
            inputAction.PlayerActions.RS.performed += i => rs =true; 

            if (rs) {
                playerInventory.ChangeRightWeapon();
            }
        }

        private void HandleInteractableButtonInput()
        {
            inputAction.PlayerActions.G.performed += inputAction => G_input = true; 


        }
    }
}