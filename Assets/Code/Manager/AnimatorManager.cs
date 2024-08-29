using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class AnimatorManager : MonoBehaviour
    {
        public Animator anim;

        public void PlayTargetAnimation(string targetAnim, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool("IsInteract", isInteracting);
            anim.CrossFade(targetAnim, 0.2f);
        }
    }
}
