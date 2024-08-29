using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    [CreateAssetMenu(menuName = "a.i/enemyaction/attack action")]
    public class EnemyAttack : EnemyAction
    {
        public int attackscore = 3;
        public float recoverytime = 2;
        public float maximumAttackAngle = 40;
        public float minimumAttackAngle = -40;

        public float minimumDistanceNeedToAttack = 0;
        public float maximumDistanceNeedToAttack = 1;
    }
}
