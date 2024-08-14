using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace HQ {
    public class Player : MonoBehaviour
    {
        PlayerManager playerManager;
        Transform cameraObject;
        InputHandler inputHandler;
        Vector3 moveDirection;

        [HideInInspector]
        public Transform myTransform;
        [HideInInspector]
        public AnimatorHandler animatorHandler;

        public new Rigidbody rigidbody;
        public GameObject normalCamera;

        [Header("Movement Stats")]
        [SerializeField]
        float movementspeed = 5;
        [SerializeField]
        float sprintSpeed = 7;
        [SerializeField]
        float rotationspeed = 10;

        void Start()
        {
            playerManager = GetComponent<PlayerManager>();
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            cameraObject = Camera.main.transform;
            myTransform = transform;
            animatorHandler.Initialize();
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

            float speed = movementspeed;

            if (inputHandler.sprintFlag && inputHandler.moveAmount > 0.5f) {
                speed = sprintSpeed;
                playerManager.IsSprinting = true;
                moveDirection *= speed;
            }
            else {
                if (inputHandler.moveAmount < 0.5f) {
                    playerManager.IsSprinting = false;
                }
                else {
                moveDirection *= speed;
                }
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

            if (inputHandler.RollFlag) {
                moveDirection = cameraObject.forward * inputHandler.vertical;
                moveDirection += cameraObject.right * inputHandler.horizontal;

                if(inputHandler.moveAmount > 0) {
                    animatorHandler.PlayTargetAnimation("Roll", true);
                    moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = rollRotation;
                }
                else {
                    animatorHandler.PlayTargetAnimation("Stepback", true);
                }
            }
        }
        #endregion
    }
}