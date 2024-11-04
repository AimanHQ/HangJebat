using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace HQ
{
    public class RespawnManager : MonoBehaviour
    {
        public Transform respawnPoint; // Set this to the respawn location in the scene
        public GameObject player;      // Reference to the player GameObject
        public GameObject respawnUI;   // UI element for respawn prompt (optional)

        private Characterstats playerStats;

        private void Start()
        {
            if (player == null)
            {
                Debug.LogError("Player object not assigned to RespawnManager.");
            }

            playerStats = player.GetComponent<Characterstats>();

            // Ensure UI is hidden at the start
            if (respawnUI != null)
            {
                respawnUI.SetActive(false);
            }
        }

        // Method to trigger when the player dies
        public void HandlePlayerDeath()
        {
            // Show respawn UI if exists
            if (respawnUI != null)
            {
                respawnUI.SetActive(true);
            }

            // Alternatively, you could instantly respawn after a short delay without UI
            StartCoroutine(RespawnAfterDelay(3f));
        }

        public void RespawnPlayer()
        {
            // Hide respawn UI
            if (respawnUI != null)
            {
                respawnUI.SetActive(false);
            }

            // Move player to the respawn point
            player.transform.position = respawnPoint.position;

            // Reset player's health or stats as needed
            playerStats.currenthealth = playerStats.MaxHealth;
            playerStats.isDead = false;

            // Trigger respawn animation if any (assuming Animator is on the player)
            Animator animator = player.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.Play("RespawnAnimation"); // Make sure you have a "RespawnAnimation"
            }
        }

        private IEnumerator RespawnAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            RespawnPlayer();
        }
    }
}
