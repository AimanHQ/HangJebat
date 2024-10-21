using HQ;
using UnityEngine;

namespace Scene_Teleportation_Kit.Scripts.teleport
{
    public class Teleporter : MonoBehaviour {
        public Transform targetPosition;  // Set this to the teleport destination
        public string destSpawnName;

        void OnTriggerEnter(Collider collider) {
            Teleportable teleportable = collider.GetComponent<Teleportable>();
            if (teleportable != null) {
                OnEnter(teleportable);
            }
        }

        public void OnEnter(Teleportable teleportable) {
            if (!teleportable.canTeleport) {
                return;
            }
            teleportable.canTeleport = false;

            // Teleport player to the target position in the same scene
            Teleport(teleportable);
        }

        private void Teleport(Teleportable teleportable) {
            // Check if the target position is valid
            if (targetPosition != null) {
                teleportable.GetComponent<Player>().TeleportTo(targetPosition);
            } else {
                Debug.LogWarning("Target position for teleport is not set!");
            }

            teleportable.canTeleport = true; // Allow further teleportation
        }
    }
}
