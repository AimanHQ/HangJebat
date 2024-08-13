using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class WeaponSlotManager : MonoBehaviour
    {
        WeaponHolderSlot lefthandslot;
        WeaponHolderSlot righthandslot;

        private void Awake()
        {
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
            }
            else 
            {
                righthandslot.LoadWeaponModel(weaponItems);
            }
        }
    }
}
