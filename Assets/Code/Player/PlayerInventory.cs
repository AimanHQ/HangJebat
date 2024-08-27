using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class PlayerInventory : MonoBehaviour
    {
        WeaponSlotManager weaponSlotManager;
        public WeaponItems rightweapon;
        public WeaponItems leftweapon;
        public WeaponItems unarmedWeapon;

        public WeaponItems[] weaponInRightHandslot = new WeaponItems[1];
        public WeaponItems[] weaponInLeftHandslot = new WeaponItems[1];

        public int currentRightWeaponIndex = -1;
        public int currentLeftWeaponIndex = -1;

        public List<WeaponItems> weaponInventory;

        private void Awake()
        {
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();

        }

        private void Start()
        {
            rightweapon = unarmedWeapon;
            leftweapon = unarmedWeapon;

            /*rightweapon =  weaponInRightHandslot[currentRightWeaponIndex];
            leftweapon = weaponInLeftHandslot[currentLeftWeaponIndex];
            weaponSlotManager.LoadWeaponOnSlot(rightweapon, false);
            weaponSlotManager.LoadWeaponOnSlot(leftweapon, true);*/
        }

        public void ChangeRightWeapon()
        {
            if (currentRightWeaponIndex < weaponInRightHandslot.Length - 1)
            {
                currentRightWeaponIndex = currentRightWeaponIndex + 1;
            }
            else
                currentRightWeaponIndex = -1;

            if (currentRightWeaponIndex == 0 && weaponInRightHandslot[0] != null)
            {
                rightweapon = weaponInRightHandslot[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponInRightHandslot[currentRightWeaponIndex], false);
            }
            else if(currentRightWeaponIndex == 0 && weaponInRightHandslot[0] == null)
            {
                currentRightWeaponIndex = currentRightWeaponIndex + 1;
            }
            
            else if (currentRightWeaponIndex == 1 && weaponInRightHandslot[1] != null)
            {
                rightweapon = weaponInRightHandslot[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponInRightHandslot[currentRightWeaponIndex], false);
            }

            if (currentRightWeaponIndex == -1)
            {
                rightweapon = unarmedWeapon;
                weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, false);
            }
        }
    }
}
