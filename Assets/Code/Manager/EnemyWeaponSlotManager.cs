using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class EnemyWeaponSlotManager : MonoBehaviour
    {
        public WeaponItems righthandWeapon;
        public WeaponItems lefthandWeapon;
        WeaponHolderSlot rightWeaponSlot;
        WeaponHolderSlot leftWeaponSlot;

        DamageCollider leftHandDamageCollider;
        DamageCollider rightHandDamageCollider;
        EnemyFxManager enemyFxManager;

        private EnemyStats enemyStats;

        private void Awake()
        {
            enemyFxManager = GetComponent<EnemyFxManager>();
            enemyStats = GetComponentInParent<EnemyStats>();

            WeaponHolderSlot[] WeaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in WeaponHolderSlots) 
            {
                if (weaponSlot.islefthandslot)
                {
                    leftWeaponSlot = weaponSlot;
                }
                else if (weaponSlot.issrighthandslot)
                {
                    rightWeaponSlot = weaponSlot;
                }
            }
        }

        private void Start()
        {
            LoadWeaponOnBothHand();

            // Subscribe to OnDeath event to unequip weapons when the enemy dies
            enemyStats.OnDeath += UnequipAllWeapons;
        }

        public void LoadWeaponOnSlot(WeaponItems weaponItems, bool isleft)
        {
            if (isleft) 
            {
                leftWeaponSlot.currentWeapon = weaponItems;
                leftWeaponSlot.LoadWeaponModel(weaponItems);
                //load weapon dmg collider
                LoadWeaponDamageCollider(true); 
            }
            else 
            {
                rightWeaponSlot.currentWeapon = weaponItems;    
                rightWeaponSlot.LoadWeaponModel(weaponItems);
                //load weapon dmg collider
                LoadWeaponDamageCollider(false);    
            }
        }

        public void LoadWeaponOnBothHand()
        {
            if (righthandWeapon != null)
            {
                LoadWeaponOnSlot(righthandWeapon, false);   
            }
            if (lefthandWeapon != null)
            {
                LoadWeaponOnSlot(lefthandWeapon, true);
            }
        }

        public void LoadWeaponDamageCollider(bool isleft)
        {
            if (isleft)
            {
                leftHandDamageCollider = leftWeaponSlot.currentweaponmodel.GetComponentInChildren<DamageCollider>();
                enemyFxManager.leftWeaponFX = leftWeaponSlot.currentweaponmodel.GetComponentInChildren<WeaponFX>();
            }
            else 
            {
                rightHandDamageCollider = rightWeaponSlot.currentweaponmodel.GetComponentInChildren<DamageCollider>();
                enemyFxManager.rightWeaponFX = rightWeaponSlot.currentweaponmodel.GetComponentInChildren<WeaponFX>();
            }
        }

        public void OpenDamageCollider()
        {
            rightHandDamageCollider.EnableDamageCollider();
        }

        public void CloseDamageCollider()
        {
            rightHandDamageCollider.DisableDamageCollider();
        }

        // Method to unequip all weapons when the enemy dies
        private void UnequipAllWeapons()
        {
            if (rightWeaponSlot != null)
            {
                rightWeaponSlot.UnloadWeaponAndDestroy();
            }
            if (leftWeaponSlot != null)
            {
                leftWeaponSlot.UnloadWeaponAndDestroy();
            }
            Debug.Log("All weapons have been unequipped.");
        }

       public void DrainStaminaLightAttack()
        {
            
        }

        public void DrainStaminaHeavyAttack()
        {

        }
    }
}
