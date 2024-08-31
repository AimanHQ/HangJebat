using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class WeaponSlotManager : MonoBehaviour
    {
        PlayerManager playerManager;
        WeaponHolderSlot lefthandslot;
        WeaponHolderSlot righthandslot;

        DamageCollider lefthanddamagecollider;
        DamageCollider righthanddamagecollider;
        public WeaponItems attackingweapon;

        PlayerStats playerStats;

        private void Awake()
        {
            playerManager = GetComponentInParent<PlayerManager>();
            playerStats = GetComponentInParent<PlayerStats>();

            WeaponHolderSlot[] WeaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in WeaponHolderSlots) 
            {
                if (weaponSlot.islefthandslot)
                {
                    lefthandslot = weaponSlot;
                }
                else if (weaponSlot.issrighthandslot)
                {
                    righthandslot = weaponSlot;
                }
            }
        }
        public void LoadWeaponOnSlot(WeaponItems weaponItems, bool isleft)
        {
            if (isleft) 
            {
                lefthandslot.currentWeapon = weaponItems;
                lefthandslot.LoadWeaponModel(weaponItems);
                LoadLeftweapondamagecollider();
            }
            else 
            {
                righthandslot.currentWeapon =  weaponItems;
                righthandslot.LoadWeaponModel(weaponItems);
                Loadrightweapondamagecollider();
            }
        }

        #region Handle weapon dmg collider
        private void LoadLeftweapondamagecollider()
        {
            lefthanddamagecollider = lefthandslot.currentweaponmodel.GetComponentInChildren<DamageCollider>();
        }

        private void Loadrightweapondamagecollider()
        {
            righthanddamagecollider = righthandslot.currentweaponmodel.GetComponentInChildren<DamageCollider>();
        }

        public void OpenDamageCollider()
        {
            if (playerManager.isUsingRightHand)
            {
                righthanddamagecollider.EnableDamageCollider();
            }
            else if (playerManager.isUsingLeftHand)
            {
                lefthanddamagecollider.EnableDamageCollider();  
            }
        }
    
        public void CloseDamageCollider()
        {
            righthanddamagecollider.DisableDamageCollider();
            //lefthanddamagecollider.DisableDamageCollider(); 
        }
        #endregion
   
        #region handle weapon stamina
        public void DrainStaminaLightAttack()
        {
            playerStats.TakeStaminaDamage(Mathf.RoundToInt(attackingweapon.baseStamina * attackingweapon.lightAttackMultiplier));
        }

        public void DrainStaminaHeavyAttack()
        {
            playerStats.TakeStaminaDamage(Mathf.RoundToInt(attackingweapon.baseStamina * attackingweapon.HeavyAttackMultiplier));
        }
        #endregion
    }
}
