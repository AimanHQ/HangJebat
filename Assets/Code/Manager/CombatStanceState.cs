using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class CombatStanceState : State
    {
        public AttackStates attackStates;
        public PersueTargetState persueTargetState;
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorHandler enemyAnimatorHandler)
        {
            //if in range attack, return attack state
            //if cooldown after attack, returhn state and circle player
            //if player run out range, return persue target

            //potential circle player or walk around them
            enemyManager.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            
            //check attack range 
            if (enemyManager.currentRecoveryTime <= 0 && enemyManager.distanceFromTarget <= enemyManager.maximumAttackRange)
            {
                return attackStates;
            }
            else if (enemyManager.distanceFromTarget > enemyManager.maximumAttackRange)
            {
                return persueTargetState;
            }
            else 
            {
            return this;
            }
        }
    }
}
