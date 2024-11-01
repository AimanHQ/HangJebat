using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniVRM10;

namespace HQ
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;
        InputHandler inputHandler;
        PlayerInventory playerInventory;
        PlayerManager playerManager;
        PlayerStats playerStats;
        PlayerEffectManager playerEffectManager;

        public string LastAttack;

        private void Awake()
        {
            animatorHandler = GetComponent<AnimatorHandler>();
            playerManager = GetComponentInParent<PlayerManager>();
            playerStats = GetComponentInParent<PlayerStats>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            playerEffectManager = GetComponent<PlayerEffectManager>();
            inputHandler = GetComponentInParent<InputHandler>();
        }

        public void HandleWeaponCombo(WeaponItems weapon)
        {
            if (inputHandler.ComboFlag)
            {
            animatorHandler.anim.SetBool("CanDoCombo", false);
            if (LastAttack == weapon.Oh_Light_Attack1)
            {
                animatorHandler.PlayTargetAnimation(weapon.Oh_Light_Attack2, true);
            }                
            }

        }
        public void HandleLightAttack(WeaponItems weapon)
        {
            if (playerStats.currentstamina <= 0)
                return;

            animatorHandler.PlayTargetAnimation(weapon.Oh_Light_Attack1, true);
            LastAttack = weapon.Oh_Light_Attack1;
        }

        public void HandleHeavyAttack(WeaponItems weapon)
        {
            if (playerStats.currentstamina <= 0)
                return;
                
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
        public void HandleHeal()
        {
            playerEffectManager.HealPlayerFromEffect(); // Use your existing method for healing
            playerInventory.RevertToPreviousRightWeapon(); // Revert back to the previous weapon
        }

        #endregion

        #region  attack action
        private void performRBmeleeAction()
        {
            animatorHandler.anim.SetBool("isUsingRightHand", true);
            HandleLightAttack(playerInventory.rightweapon);

            //play fx
            playerEffectManager.PlayWeaponFx(false);    
        }

        private void performRBmagicAction(WeaponItems weapon)
        {
            if (playerManager.isInteracting)
                return;

            if (weapon.isFaithSpell)
            {
                if (playerInventory.currentSpell != null && playerInventory.currentSpell.isFaithSpell)
                {
                    //check for mana
                    //attempt to cast spell

                    if (playerStats.currentMana >= playerInventory.currentSpell.ManaCost)
                    {
                    playerInventory.currentSpell.AttemptToCastSpell(animatorHandler, playerStats);
                    }
                    else 
                    {
                        animatorHandler.PlayTargetAnimation("shrug", true);
                    }
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
