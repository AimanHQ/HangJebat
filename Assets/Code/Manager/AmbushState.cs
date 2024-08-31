using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class AmbushState : State
    {
        public bool isSleeping;
        public float detectionRadius = 2;
        public string SleepAnimation;
        public string wakeAnimation;
        public LayerMask detectionLayer;

        public PersueTargetState persueTargetState;
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorHandler enemyAnimatorHandler)
        {  
            if (isSleeping && enemyManager.isInteracting == false)
            {
                enemyAnimatorHandler.PlayTargetAnimation(SleepAnimation, true);
            }

            #region handle target detect
            Collider[] colliders = Physics.OverlapSphere(enemyManager.transform.position, detectionRadius, detectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                Characterstats characterstats = colliders[i].transform.GetComponent<Characterstats>();

                if (characterstats != null)
                {
                    Vector3 targetDirection = characterstats.transform.position - enemyManager.transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

                    if (viewableAngle > enemyManager.MinimumDetectionAngle
                        && viewableAngle < enemyManager.maximumDetectionAngle)
                        {
                            enemyManager.currentTarget = characterstats;
                            isSleeping = false;
                            enemyAnimatorHandler.PlayTargetAnimation(wakeAnimation, true);
                        }
                }
            }
            #endregion

            #region handle state change
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
