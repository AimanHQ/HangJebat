using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class PlayerEffectManager : CharacterEffectManager
    {
        PlayerStats playerStats;
        public GameObject currentparticleFx; //particle effect that will play effect player
        PlayerInventory playerInventory; // Reference to PlayerInventory
        public GameObject instatiateFX;
        Animator playerAnimator; // Animator on the player for animations

        public int AmountToBeHeal;

        private void Awake()
        {
            playerStats = GetComponentInParent<PlayerStats>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            playerAnimator = GetComponentInParent<Animator>(); // Get Animator on the Player GameObject
        }
        public void HealPlayerFromEffect()
        {
            if (playerInventory != null)
            {
                // Switch to magic item to trigger healing
                playerInventory.SwitchToMagicItemForHealing();
            }

             // Play the healing animation if the animator is available
            if (playerAnimator != null)
            {
                playerAnimator.Play("Heal"); // Make sure "HealingAnimation" is the correct animation name
                Debug.Log("Healing animation triggered.");
            }
            else
            {
                Debug.LogWarning("Player Animator not found!");
            }

            // Apply healing directly
            if (playerStats != null)
            {
                playerStats.healPlayer(AmountToBeHeal);
                Debug.Log("Player healed by " + AmountToBeHeal);
            }

            // Instantiate the particle effect if assigned
            if (currentparticleFx != null)
            {
                GameObject healParticles = Instantiate(currentparticleFx, playerStats.transform.position, Quaternion.identity);
                Destroy(healParticles, 5f); // Optional: Destroy particle effect after 5 seconds
            }
            else
            {
                Debug.LogWarning("currentparticleFx is not assigned in the PlayerEffectManager inspector.");
            }
        }

    }
}
