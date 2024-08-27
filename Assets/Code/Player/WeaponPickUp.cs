using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class WeaponPickUp : Interactable
    {
        public WeaponItems weapon;

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            //pick up weapon and add to player inventory
            PickUpItem(playerManager);
        }

        private void PickUpItem(PlayerManager playerManager)
        {
            PlayerInventory playerInventory;
            Player playerlocomotion;
            AnimatorHandler animatorHandler;

            playerInventory = playerManager.GetComponent<PlayerInventory>();
            playerlocomotion = playerManager.GetComponent<Player>();
            animatorHandler = playerManager.GetComponentInChildren<AnimatorHandler>();

            playerlocomotion.rigidbody.velocity =Vector3.zero; //stop player from moving while pick up item
            animatorHandler.PlayTargetAnimation("Picking Up", true); //play animation pick up item
            playerInventory.weaponInventory.Add(weapon);
            Destroy(gameObject); 
        }
    }
}
