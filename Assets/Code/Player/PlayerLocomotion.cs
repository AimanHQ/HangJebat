using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HQ {
    public class Player : MonoBehaviour
    {
        PlayerManager playerManager;
        PlayerStats playerStats;
        Transform cameraObject;
        InputHandler inputHandler;
        public FadeController fadeController;
        public Vector3 moveDirection;

        [HideInInspector]
        public Transform myTransform;
        [HideInInspector]
        public AnimatorHandler animatorHandler;

        public new Rigidbody rigidbody;
        public GameObject normalCamera;

        [Header("Ground/air detect stats")]
        [SerializeField] float grounddetectraystartpoint = 0.5f;
        [SerializeField] float minimumdistancetofall = 1f;
        [SerializeField] float grounddirectiontraydistance = 0.2f;
        LayerMask ignoreforgroundcheck;
        public float inAirTimer;



        [Header("Movement Stats")]
        [SerializeField]
        float movementspeed = 5;
        [SerializeField]
        float sprintSpeed = 7;
        [SerializeField]
        float rotationspeed = 10;
        [SerializeField] float fallingspeed = 45;

        [Header("stamina Cost")]
        [SerializeField]
        int rollStaminaCost = 15;
        int sprintStaminaCost = 1;

        [Header("stamina regen")]
        [SerializeField]
        int staminaregen = 1;

        public CapsuleCollider charactercollider;   
        public CapsuleCollider charactercollionblocker;

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
            playerStats = GetComponent<PlayerStats>();
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }

        void Start()
        {
            cameraObject = Camera.main.transform;
            myTransform = transform;
            animatorHandler.Initialize();
            playerManager.isGrounded = true;
            ignoreforgroundcheck = ~(1 << 8 | 1 << 11);

            Physics.IgnoreCollision(charactercollider, charactercollionblocker, true);
        }

        #region movement
        Vector3 normalVec;
        Vector3 targetPos;

        private void HandleRotation(float delta)
        {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = inputHandler.moveAmount;

            targetDir = cameraObject.forward * inputHandler.vertical;
            targetDir += cameraObject.right * inputHandler.horizontal;

            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
                targetDir = myTransform.forward;

            float rs = rotationspeed;
            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation =  Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

            myTransform.rotation = targetRotation;
        }

        public void HandleMovement (float delta)
        {
            if (inputHandler.RollFlag) 
                return;

            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;
            moveDirection.Normalize();
            moveDirection.y = 0;

            if (playerManager.isInteracting)
                return;

            float speed = movementspeed;

            if (inputHandler.sprintFlag && inputHandler.moveAmount > 0.5f) {
                speed = sprintSpeed;
                playerManager.IsSprinting = true;
                moveDirection *= speed;
                playerStats.TakeStaminaDamage(sprintStaminaCost);
            }
            else {
                if (inputHandler.moveAmount < 0.5f) {
                    playerManager.IsSprinting = false;
                }
                else {
                moveDirection *= speed;
                }

                playerStats.RegenMana(staminaregen);
            }

            Vector3 projectedvelocity = Vector3.ProjectOnPlane(moveDirection, normalVec);
            rigidbody.velocity = projectedvelocity;

            animatorHandler.UpdateAnimatorValue(inputHandler.moveAmount, 0, playerManager.IsSprinting);

            if(animatorHandler.canRotate) {
                HandleRotation(delta);
            }
        }
    
        public void HandleRollandSprint(float delta)
        {
            if(animatorHandler.anim.GetBool("IsInteract")) 
                return;

            //check if have stamina , if not return 
            if (playerStats.currentstamina <= 0)
                return;

            if (inputHandler.RollFlag) {
                moveDirection = cameraObject.forward * inputHandler.vertical;
                moveDirection += cameraObject.right * inputHandler.horizontal;

                if(inputHandler.moveAmount > 0) {
                    animatorHandler.PlayTargetAnimation("Roll", true);
                    moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = rollRotation;
                    playerStats.TakeStaminaDamage(rollStaminaCost);
                }
                else {
                    animatorHandler.PlayTargetAnimation("Stepback", true);
                }
            }
        }
        
        public void HandleFalling(float delta, Vector3 moveDirection)
        {
            playerManager.isGrounded =false;
            RaycastHit hit;
            Vector3 origin = myTransform.position;
            origin.y += grounddetectraystartpoint;

            if (Physics.Raycast(origin, myTransform.forward,out hit, 0.4f))
            {
                moveDirection = Vector3.zero;
            }
            if (playerManager.isInAir)
            {
                rigidbody.AddForce(-Vector3.up * fallingspeed);
                rigidbody.AddForce(moveDirection * fallingspeed / 10f);
            }

            Vector3 dir = moveDirection;
            dir.Normalize();
            origin = origin + dir * grounddirectiontraydistance;

            targetPos = myTransform.position;

            Debug.DrawRay(origin, -Vector3.up * minimumdistancetofall, Color.red, 0.1f, false);
            if(Physics.Raycast(origin, -Vector3.up, out hit, minimumdistancetofall, ignoreforgroundcheck))
            {
                normalVec = hit.normal;
                Vector3 tp = hit.point;
                playerManager.isGrounded = true;
                targetPos.y = tp.y;

                if (playerManager.isInAir) 
                {
                    if (inAirTimer > 0.5f) {
                        Debug.Log("you were in air" + inAirTimer);
                        inAirTimer = 0;
                    }
                    else {
                        animatorHandler.PlayTargetAnimation("New State", false);
                        inAirTimer = 0;
                    }

                    playerManager.isInAir = false;   
                }
            }
            else 
            {
                if (playerManager.isGrounded)
                {
                    playerManager.isGrounded = false;
                }
                if (playerManager.isInAir == false) 
                {
                    if(playerManager.isInteracting == false) {
                        
                    }
                    Vector3 vel = rigidbody.velocity;
                    vel.Normalize();
                    rigidbody.velocity = vel * (movementspeed / 2);
                    playerManager.isInAir = true;

                }
            }
            if (playerManager.isGrounded)
            {
                if(playerManager.isInteracting || inputHandler.moveAmount > 0) {
                    myTransform.position = Vector3.Lerp(myTransform.position, targetPos, Time.deltaTime);
                }
                else {
                    myTransform.position = targetPos;
                }
            }
            if (playerManager.isInteracting || inputHandler.moveAmount > 0) {
                myTransform.position = Vector3.Lerp(myTransform.position, targetPos, Time.deltaTime / 0.1f);
            }
            else {
                myTransform.position = targetPos;
            }
        }

        public void OnMove(InputValue value)
        {
            moveDirection = value.Get<Vector2>();
        }

public void TeleportTo(Transform destination)
{
    StartCoroutine(TeleportSequence(destination));
}

private IEnumerator TeleportSequence(Transform destination)
{
    // Step 1: Fade to black before teleporting
    fadeController.FadeIn();
    yield return new WaitForSeconds(fadeController.fadeDuration); // Wait for fade-in to complete

    // Step 2: Teleport the player
    transform.position = destination.position;

    // Step 3: Fade back in from black after teleporting
    fadeController.FadeOut();
}

        
        #endregion
    }
}