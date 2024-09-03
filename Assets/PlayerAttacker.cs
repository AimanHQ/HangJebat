using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;
        PlayerInventory playerInventory;
        PlayerManager playerManager;
        PlayerStats playerStats;

        private void Awake()
        {
            animatorHandler = GetComponent<AnimatorHandler>();
            playerManager = GetComponentInParent<PlayerManager>();
            playerStats = GetComponentInParent<PlayerStats>();
            playerInventory = GetComponentInParent<PlayerInventory>();
        }
        public void HandleLightAttack(WeaponItems weapon)
        {
            animatorHandler.PlayTargetAnimation(weapon.Oh_Light_Attack1, true);
        }

        public void HandleHeavyAttack(WeaponItems weapon)
        {
            animatorHandler.PlayTargetAnimation(weapon.Oh_Heavy_Attack1, true);
        }
        #region  input action
        public void HandleRBAction()
        {
            if (playerInventory.rightweapon.isMeleeWerapon)
            {
                //handle melee action
                performRBmeleeAction();
            }
            else if (playerInventory.rightweapon.isFaithSpell || playerInventory.rightweapon.isMagicSpell)
            {
                //handle miracle action
                performRBmagicAction(playerInventory.rightweapon);
            }
        }
        #endregion

        #region  attack action
        private void performRBmeleeAction()
        {
            animatorHandler.anim.SetBool("isUsingRightHand", true);
            HandleLightAttack(playerInventory.rightweapon);
        }

        private void performRBmagicAction(WeaponItems weapon)
        {
            if (weapon.isFaithSpell)
            {
                if (playerInventory.currentSpell != null && playerInventory.currentSpell.isFaithSpell)
                {
                    //check for mana
                    //attempt to cast spell
                    playerInventory.currentSpell.AttemptToCastSpell(animatorHandler, playerStats);
                }
            }
        }

        public  void SuccessfulyCastSpell()
        {
            playerInventory.currentSpell.SuccessfulyCastSpell(animatorHandler, playerStats);
        }

        #endregion
    }
}
