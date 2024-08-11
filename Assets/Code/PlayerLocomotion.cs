using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ {
    public class Player : MonoBehaviour
    {
        Transform cameraObject;
        InputHandler inputHandler;
        Vector3 moveDirection;

        [HideInInspector]
        public Transform myTransform;
        [HideInInspector]
        public AnimatorHandler animatorHandler;

        public new Rigidbody rigidbody;
        public GameObject normalCamera;

        [Header("Stats")]
        [SerializeField]
        float movementspeed = 5;
        [SerializeField]
        float rotationspeed = 10;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            cameraObject = Camera.main.transform;
            myTransform = transform;
            animatorHandler.Initialize();
        }

        public void Update()
        {
            float delta = Time.deltaTime;

            inputHandler.TickInput(delta);

            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;
            moveDirection.Normalize();
            moveDirection.y = 0;

            float speed = movementspeed;
            moveDirection *= speed;

            Vector3 projectVelocity = Vector3.ProjectOnPlane(moveDirection, normalVec);
            rigidbody.velocity = projectVelocity;

            animatorHandler.UpdateAnimatorValue(inputHandler.moveAmount, 0);

            if (animatorHandler.canRotate) {
                HandleRotation(delta);
            }
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
        #endregion
    }
}