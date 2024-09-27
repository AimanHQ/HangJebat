using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class WeaponFX : MonoBehaviour
    {
        [Header("Weaapon FX")]
        public ParticleSystem normalWeaponTrail;
        //dark weapon trail
        //magic weapon trail

        public void PlayWeaponFx()
        {
            normalWeaponTrail.Stop();

            if (normalWeaponTrail.isStopped)
            {
                normalWeaponTrail.Play();
            }
        }
    }
}
