using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace HQ 
{
    public class WeaponHolderSlot : MonoBehaviour
    {
        public Transform parrentoverride;
        public bool islefthandslot;
        public bool issrighthandslot;

        public GameObject currentweaponmodel;

        public void UnloadWeapon()
        {
            if (currentweaponmodel != null) {
                currentweaponmodel.SetActive(false);
            }
        }

        public void UnloadWeaponAndDestroy()
        {
            if (currentweaponmodel != null) {
                Destroy(currentweaponmodel);
            }
        }

        public void LoadWeaponModel(WeaponItems weaponItems)
        {
            UnloadWeaponAndDestroy();

            if (weaponItems == null) {
                UnloadWeapon();
                return;
            }

            GameObject model = Instantiate(weaponItems.prefabModel) as GameObject;
            if (model != null) {
                if (parrentoverride != null)
                {
                    model.transform.parent = parrentoverride;
                }
                else 
                {
                    model.transform.parent = transform;
                }

                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one;
            }

            currentweaponmodel = model;
        }
    }
}
