using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HQ
{
    public class RespawnManager : MonoBehaviour
    {
        [SerializeField] private string respawnSceneName = "RespawnScene"; // Set this to your respawn scene's name
        [SerializeField] private GameObject respawnPanel; // Assign the respawn UI panel in the Inspector

        private void Start()
        {
            // Ensure the respawn UI is hidden at the start
            if (respawnPanel != null)
            {
                respawnPanel.SetActive(false);
            }
        }

    public void HandlePlayerDeath()
        {
            // Show the respawn UI when the player dies and pause the game
            if (respawnPanel != null)
            {
                respawnPanel.SetActive(true);
            }
        }

        public void OnRespawnButtonClicked()
        {
            // Resume game time, hide the respawn UI, and load the respawn scene
            if (respawnPanel != null)
            {
                respawnPanel.SetActive(false);
            }
            SceneManager.LoadScene(respawnSceneName);
        }
    }
}

