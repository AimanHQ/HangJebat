using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class WeaponFX : MonoBehaviour
    {
        [Header("Weapon FX")]
        public ParticleSystem normalWeaponTrail;
        public Transform playerTransform;  // Reference to the player's transform

        private Quaternion initialRotation;
        private Vector3 initialOffset;

        void Start()
        {
            if (normalWeaponTrail != null && playerTransform != null)
            {
                // Store the initial rotation of the particle system
                initialRotation = normalWeaponTrail.transform.rotation;

                // Calculate the initial offset of the particle system relative to the player
                initialOffset = normalWeaponTrail.transform.position - playerTransform.position;
            }
        }

        public void PlayWeaponFx()
        {
            if (normalWeaponTrail != null)
            {
                normalWeaponTrail.Stop();

                if (normalWeaponTrail.isStopped)
                {
                    // Play the particle system
                    normalWeaponTrail.Play();
                }
            }
        }

        void LateUpdate()
        {
            if (normalWeaponTrail != null && playerTransform != null)
            {
                // Lock the particle system rotation to its initial rotation
                normalWeaponTrail.transform.rotation = initialRotation;

                // Update the particle system position to follow the player's position with an offset
                normalWeaponTrail.transform.position = playerTransform.position + initialOffset;
            }
        }
    }
}
