using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ 
{
    public class AnimatorHandler : AnimatorManager
    {
        PlayerManager playerManager;
        InputHandler inputHandler;
        Player PlayerLocomotion;
        int vertical;
        int horizontal;
        public bool canRotate;

        public void Initialize()
        {
            playerManager =  GetComponentInParent<PlayerManager>();
            anim = GetComponent<Animator>();
            inputHandler = GetComponentInParent<InputHandler>();
            PlayerLocomotion = GetComponentInParent<Player>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        public void UpdateAnimatorValue(float verticalMovement,float horizontalMovement, bool IsSprinting)
        {
            #region Vertical
            float v =0;

            if (verticalMovement >0 && verticalMovement < 0.55f) {
                v = 0.5f;
            }
            else if (verticalMovement > 0.55f) {
                v = 1;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f) {
                v= -0.5f;
            }
            else if (verticalMovement < -0.55f) {
                v = -1;
            }
            else {
                v = 0;
            }
            #endregion

            #region Horizontal
            float h = 0;
            if (horizontalMovement >0 && horizontalMovement < 0.55f) {
                h = 0.5f;
            }
            else if (horizontalMovement > 0.55f) {
                h = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f) {
                h= -0.5f;
            }
            else if (horizontalMovement < -0.55f) {
                h = -1;
            }
            else {
                h = 0;
            }
            #endregion

            if (IsSprinting) {
                v = 2;
                h = horizontalMovement;
            }
            
            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

        public void CanRotate()
        {
            canRotate = true;
        }

        public void stopRotation()
        {
            canRotate = false;
        }

        private void OnAnimatorMove()
        {
            if (playerManager.isInteracting == false)
                return;

            float delta = Time.deltaTime;
            PlayerLocomotion.rigidbody.drag = 0;
            Vector3 deltaposition = anim.deltaPosition;
            deltaposition.y = 0;
            Vector3 velocity = deltaposition / delta;
            PlayerLocomotion.rigidbody.velocity = velocity;
        }
    }
}
