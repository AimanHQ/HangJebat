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
        InteractableUI interactableUI;
        public GameObject interactableUIGameObject;

        public bool isInteracting;
        [Header ("Player Flags")]
        public bool IsSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool isUsingRightHand;   
        public bool isUsingLeftHand;
        public bool canDoCombo; 


        private void Awake()
        {
            
        }
        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            player = GetComponent<Player>();
            cameraHandler = CameraHandler.singleton;
            interactableUI = FindObjectOfType<InteractableUI>();
        }
        void Update()
        {
            float delta = Time.deltaTime;
            isInteracting = anim.GetBool("IsInteract");
            canDoCombo = anim.GetBool("CanDoCombo");

            inputHandler.TickInput(delta);
            player.HandleMovement(delta);
            player.HandleRollandSprint(delta);
            player.HandleFalling(delta, player.moveDirection);
            isUsingRightHand = anim.GetBool("isUsingRightHand");
            isUsingLeftHand = anim.GetBool("isUsingLeftHand");

            CheckForInteractableObject();
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
            inputHandler.rb_input = false;
            inputHandler.rt_input = false;
            inputHandler.rs = false;
            inputHandler.G_input = false;

            if (isInAir) {
                player.inAirTimer = player.inAirTimer + Time.deltaTime;
            }
        }

        public void CheckForInteractableObject()
        {
            RaycastHit hit;

            if (Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f, cameraHandler.ignoreLayers))
            {
                if (hit.collider.tag == "Interactable")
                {
                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                    if (interactableObject != null) {
                        string interactableText = interactableObject.interactableText;
                        //set the ui text to the interactable object text
                        //set the text pop up true
                        interactableUI.interactableText.text = interactableText;
                        interactableUIGameObject.SetActive(true);

                        if(inputHandler.G_input) 
                        {
                            hit.collider.GetComponent<Interactable>().Interact(this);
                        }
                    }
                  
                }
            }
            else 
            {
                if (interactableUIGameObject != null) {
                    interactableUIGameObject.SetActive(false);
                }
            }                
            }
        }
    }

