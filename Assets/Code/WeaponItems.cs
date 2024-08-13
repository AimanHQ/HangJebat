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
    }
}