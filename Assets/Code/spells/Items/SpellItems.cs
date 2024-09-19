using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class SpellItems : Items
    {
        public GameObject spellWarmUpFX;
        public GameObject spellCastFX;

        public string spellAnimation;

        [Header("Spell Cost")]
        public int ManaCost;
        

        [Header("Spell type")]
        public bool isFaithSpell;
        public bool isMagicSpell;

        [Header("Spell description")]
        [TextArea]
        public string spellDesc;

        public virtual void  AttemptToCastSpell(AnimatorHandler animatorHandler, PlayerStats playerStats)
        {
            Debug.Log("you attempt to cast spell");
        }

        public virtual void SuccessfulyCastSpell(AnimatorHandler animatorHandler, PlayerStats playerStats)
        {
            Debug.Log ("succes at cast spell");
            playerStats.DeductMana(ManaCost);
        }
    }
}
