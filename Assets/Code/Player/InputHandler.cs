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
        public bool ComboFlag;
        public float rollInputTimer;
        PlayerControl inputAction;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        CameraHandler cameraHandler;
        AnimatorHandler animatorHandler;
        PlayerStats playerStats;
        PlayerManager playerManager;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Awake()
        {
            playerAttacker =  GetComponentInChildren<PlayerAttacker>();
            playerStats = GetComponent<PlayerStats>();
            playerInventory = GetComponent<PlayerInventory>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            playerManager = GetComponent<PlayerManager>();
        }
        public void OnEnable()
        {
            if(inputAction == null) {
                inputAction = new PlayerControl();
                inputAction.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputAction.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
                inputAction.PlayerActions.Roll.performed += i => b_input = true;
                inputAction.PlayerActions.Roll.canceled += i => b_input = false;
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
            if(b_input) {
                rollInputTimer += delta;

                if (playerStats.currentstamina <= 0)
                {
                    b_input = false;
                    sprintFlag = false;
                }

                if (moveAmount  > 0.5f && playerStats.currentstamina > 0)
                {
                    sprintFlag = true;
                }
            }
            else {
                sprintFlag = false;
                
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
            if(rb_input) 
            {
                if (playerManager.canDoCombo)
                {
                    ComboFlag = true;
                    playerAttacker.HandleWeaponCombo(playerInventory.rightweapon);
                    ComboFlag = false;
                }
                else 
                {
                 playerAttacker.HandleRBAction();
                }
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