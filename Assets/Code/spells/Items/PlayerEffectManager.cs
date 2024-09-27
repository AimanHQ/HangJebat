using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class PlayerEffectManager : CharacterEffectManager
    {
        PlayerStats playerStats;
        public GameObject currentparticleFx; //particle effect that will play effect player
        public GameObject instatiateFX;
        public int AmountToBeHeal;

        private void Awake()
        {
            playerStats = GetComponentInParent<PlayerStats>();
        }
        public void HealPlayerFromEffect(int healAmount)
        {
            playerStats.healPlayer(AmountToBeHeal);
            GameObject healparticles = Instantiate(currentparticleFx, playerStats.transform);      
            Destroy(instatiateFX); 
        }

    }
}
