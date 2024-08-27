using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ 
{
    [CreateAssetMenu(menuName ="Items/WeaponItems")]
    public class WeaponItems : Items
    {
        public GameObject prefabModel;
        public bool isUnarmed;

        [Header ("One handed attack animation")]
        public string Oh_Light_Attack1;
        public string Oh_Heavy_Attack1;

        [Header ("Stamina Cost")]
        public int baseStamina;
        public float lightAttackMultiplier;
        public float HeavyAttackMultiplier;
    }
}