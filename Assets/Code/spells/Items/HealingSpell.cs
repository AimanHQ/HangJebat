using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace HQ
{
    [CreateAssetMenu(menuName = "spells/heal spell")]
    public class HealingSpell : SpellItems
    {
        public int healAmount;

        public override void AttemptToCastSpell(AnimatorHandler animatorHandler, PlayerStats playerStats)
        {
            base.AttemptToCastSpell(animatorHandler, playerStats);
            GameObject instantiateWarmupSpellFX = Instantiate(spellWarmUpFX, animatorHandler.transform);
            animatorHandler.PlayTargetAnimation(spellAnimation, true);
            Debug.Log("attempt to cast spell");
        }

        public override void SuccessfulyCastSpell(AnimatorHandler animatorHandler, PlayerStats playerStats)
        {
            base.SuccessfulyCastSpell(animatorHandler, playerStats);
            GameObject instantiateSpellFX = Instantiate(spellCastFX, animatorHandler.transform);
            playerStats.healPlayer(healAmount);
            Debug.Log("success cast");
        }
    }   
}
