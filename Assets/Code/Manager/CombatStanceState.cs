using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class NewBehaviourScript : State
    {
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorHandler enemyAnimatorHandler)
        {
            //check attack range 
            //potential circle player or walk around them
            //if in range attack, return attack state
            //if cooldown after attack, returhn state and circle player
            //if player run out range, return persue target
            return this;
        }
    }
}
