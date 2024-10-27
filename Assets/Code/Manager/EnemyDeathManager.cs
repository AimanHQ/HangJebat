using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class EnemyDeathManager : MonoBehaviour
    {
        private List<EnemyStats> activeEnemies = new List<EnemyStats>();
        public GameObject objectToActivate; // Object to activate when all enemies are dead


        // Register a new enemy to track
        public void RegisterNewEnemy(EnemyStats enemy)
        {
            if (enemy != null && !activeEnemies.Contains(enemy))
            {
                activeEnemies.Add(enemy);
                enemy.OnDeath += () => UnregisterEnemy(enemy); // Subscribe to the enemy's death event
            }
        }

        // Unregister an enemy when it dies
        private void UnregisterEnemy(EnemyStats enemy)
        {
            if (activeEnemies.Contains(enemy))
            {
                activeEnemies.Remove(enemy);

                // Check if all enemies are dead and activate specific GameObject if needed
                if (activeEnemies.Count == 0)
                {
                    ActivateAfterAllEnemiesDefeated();
                }
            }
        }

        // This method is called when all enemies are defeated
        private void ActivateAfterAllEnemiesDefeated()
        {
            Debug.Log("All enemies defeated. Activating the specified GameObject.");
            // Activate your GameObject here, e.g., 
            objectToActivate.SetActive(true);
        }
    }
}
