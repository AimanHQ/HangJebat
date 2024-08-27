using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class WeaponSlotManager : MonoBehaviour
    {
        WeaponHolderSlot lefthandslot;
        WeaponHolderSlot righthandslot;

        DamageCollider lefthanddamagecollider;
        DamageCollider righthanddamagecollider;
        public WeaponItems attackingweapon;

        PlayerStats playerStats;

        private void Awake()
        {
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
                lefthandslot.LoadWeaponModel(weaponItems);
                LoadLeftweapondamagecollider();
            }
            else 
            {
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

        public void Openrightdamagecollider()
        {
            righthanddamagecollider.EnableDamageCollider();
        }

        public void Openleftdamagecollider()
        {
            lefthanddamagecollider.EnableDamageCollider();
        }

        public void Closerightdamagecollider()
        {
            righthanddamagecollider.DisableDamageCollider();
        }

        public void Closeleftdamagecollider()
        {
            lefthanddamagecollider.DisableDamageCollider();
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
