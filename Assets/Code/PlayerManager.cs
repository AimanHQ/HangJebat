using System.Collections;
using System.Collections.Generic;
using HQ;
using UnityEngine;

namespace HQ
{
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        Animator anim;
        CameraHandler cameraHandler;
        Player player;

        public bool isInteracting;
        [Header ("Player Flags")]
        public bool IsSprinting;


        private void Awake()
        {
            
        }
        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            player = GetComponent<Player>();
            cameraHandler = CameraHandler.singleton;
        }
        void Update()
        {
            float delta = Time.deltaTime;
            isInteracting = anim.GetBool("IsInteract");

            inputHandler.TickInput(delta);
            player.HandleMovement(delta);
            player.HandleRollandSprint(delta);
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;
            if (cameraHandler != null) {
                cameraHandler.followtarget(delta);
                cameraHandler.handlecamerarotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
        }
    
        private void LateUpdate()
        {
            inputHandler.RollFlag = false;
            inputHandler.sprintFlag = false;
            IsSprinting = inputHandler.b_input;
        }
    }
}
