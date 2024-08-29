using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class PersueTargetState : State
    {
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorHandler enemyAnimatorHandler)
        {
            //chase target
            //if within range , switch combat stance
            //if target out range , return state and chase target
            return this;
        }
    }
}
