using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class WeaponFX : MonoBehaviour
    {
        [Header("Weaapon FX")]
        public ParticleSystem normalWeaponTrail;
        private Quaternion initialRotation;


        //dark weapon trail
        //magic weapon trail

        void Start()
        {
            // Store the initial rotation of the particle system
            initialRotation = normalWeaponTrail.transform.rotation;
        }

        public void PlayWeaponFx()
        {
            normalWeaponTrail.Stop();

            if (normalWeaponTrail.isStopped)
            {
                normalWeaponTrail.Play();
            }
        }

        void LateUpdate()
        {
            // Lock the particle system rotation to its initial rotation
            if (normalWeaponTrail != null)
            {
                normalWeaponTrail.transform.rotation = initialRotation;
            }
        }
    }
}
