using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class IdleState : State
    {
        public PersueTargetState persueTargetState;
        public LayerMask detectionLayer;
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorHandler enemyAnimatorHandler)
        {
            //look for potential target
            //switch to persue target state if target found
            //if not return this
            #region  handle enemy target detection
            Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                Characterstats characterstat = colliders[i].transform.GetComponent<Characterstats>();

                if (characterstat != null)
                {
                    //check for team id
                    Vector3 targetDirection = characterstat.transform.position - transform.position;
                    float viewableAngle =Vector3.Angle(targetDirection, transform.forward);

                    if (viewableAngle > enemyManager.MinimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                    {
                        enemyManager.currentTarget = characterstat;
                    }
                }
            }
            #endregion

            #region handle switch next state
            if (enemyManager.currentTarget != null)
            {
                return persueTargetState;
            }
            else
            {
                return this;
            }
            #endregion
        }
    }
}
