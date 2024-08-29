using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class AttackStates : State
    {
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorHandler enemyAnimatorHandler)
        {
            //select of of many attack based on attack score
            //if select attack not used bcs bad angle or distance , select new attack
            //if attack viable, stop movement and attack target
            //set recovery time  to attack recovery time 
            //return combat state
            return this;
        }
    }
}
