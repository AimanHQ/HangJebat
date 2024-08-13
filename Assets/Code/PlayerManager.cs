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
        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
        }
        void Update()
        {
            inputHandler.isInteracting = anim.GetBool("IsInteract");
            inputHandler.RollFlag = false;
            inputHandler.sprintFlag = false;
        }
    }
}
