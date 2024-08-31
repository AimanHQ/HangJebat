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

        private void Awake()
        {
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
            }
            else 
            {
                rightHandDamageCollider = rightWeaponSlot.currentweaponmodel.GetComponentInChildren<DamageCollider>();
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

       public void DrainStaminaLightAttack()
        {
            
        }

        public void DrainStaminaHeavyAttack()
        {

        }
    }
}
