using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }
        public void HandleLightAttack(WeaponItems weapon)
        {
            animatorHandler.PlayTargetAnimation(weapon.Oh_Light_Attack1, true);
        }

        public void HandleHeavyAttack(WeaponItems weapon)
        {
            animatorHandler.PlayTargetAnimation(weapon.Oh_Heavy_Attack1, true);
        }
    }
}
