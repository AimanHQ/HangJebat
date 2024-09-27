using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class CharacterEffectManager : MonoBehaviour
    {
        public WeaponFX rightWeaponFX;
        public WeaponFX leftWeaponFX;
        public virtual void PlayWeaponFx(bool isLeft)
        {
            if (isLeft == false)
            {   
                //play right weapon trail
                if (rightWeaponFX != null)
                {
                    rightWeaponFX.PlayWeaponFx();
                }

            }
            else 
            {
                //play left weapon trail
                if (leftWeaponFX != null)
                {
                    leftWeaponFX.PlayWeaponFx();
                }
            }
        }
    }
}
