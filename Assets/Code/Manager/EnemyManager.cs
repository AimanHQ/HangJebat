using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace HQ
{
    public class EnemyManager : MonoBehaviour
    {
        Enemy enemylocomotion;
        EnemyAnimatorHandler enemyAnimatorHandler;
        EnemyStats enemyStats;

        public State currentState;
        public Characterstats currentTarget;

        public bool isPerfomingAction;

        [Header ("AI SETTING")]
        //higher/lower angle are, greater the enemy fov is to detect you
        public float maximumDetectionAngle = 40;
        public float MinimumDetectionAngle = -40;
        public float detectionRadius = 20;
        public float currentRecoveryTime = 0;
        private void Awake()
        {
            enemylocomotion = GetComponent<Enemy>();
            enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
            enemyStats = GetComponent<EnemyStats>();
        }
        
        private void Update()
        {
            HandleRecoveryTime();
        }

        private void FixedUpdate()
        {
            HandleStateMachine();
        }

        private void HandleStateMachine()
        {
            if (currentState != null) 
            {
                State nextState = currentState.Tick(this, enemyStats, enemyAnimatorHandler);

                if (nextState != null) 
                {
                    SwitchToNextState(nextState);
                }
            }
        }

        private void SwitchToNextState(State state)
        {
            currentState = state;
        }

        private void HandleRecoveryTime()
        {
            if (currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }
            if (isPerfomingAction)
            {
                if (currentRecoveryTime <= 0)
                {
                    isPerfomingAction = false;
                }
            }
        }

        #region Attacks
        private void AttackTarget()
        {
           /* if (isPerfomingAction)
                return;
            if (currentAttack == null)
            {
                GetNewAttack();
            }
            else 
            {
                isPerfomingAction = true;
                currentRecoveryTime = currentAttack.recoverytime;
                enemyAnimatorHandler.PlayTargetAnimation(currentAttack.actionAnimation, true);
                currentAttack = null; 
            } */
        }

        private void GetNewAttack()
        {
            /*Vector3 targetDirection = enemylocomotion.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
            enemylocomotion.distanceFromTarget = Vector3.Distance(enemylocomotion.currentTarget.transform.position, transform.position);

            int maxScore = 0;

            for (int i = 0; i < enemyattacks.Length; i++)
            {
                EnemyAttack enemyAttack = enemyattacks[i];
                if (enemylocomotion.distanceFromTarget <= enemyAttack.maximumDistanceNeedToAttack && enemylocomotion.distanceFromTarget
                    >= enemyAttack.minimumDistanceNeedToAttack)
                {
                    if (viewableAngle <= enemyAttack.maximumAttackAngle
                        && viewableAngle >= enemyAttack.minimumAttackAngle)
                    {
                        maxScore += enemyAttack.attackscore;
                    }
                }
            }

            int randomValue = Random.Range(0, maxScore);
            int tempScore = 0;

            for (int i = 0; i <  enemyattacks.Length; i++)
            {
                EnemyAttack enemyAttack = enemyattacks[i];
                if (enemylocomotion.distanceFromTarget <= enemyAttack.maximumDistanceNeedToAttack && enemylocomotion.distanceFromTarget
                    >= enemyAttack.minimumDistanceNeedToAttack)
                {
                    if (viewableAngle <= enemyAttack.maximumAttackAngle
                        && viewableAngle >= enemyAttack.minimumAttackAngle)
                    {
                        if (currentAttack != null)
                            return;

                        tempScore += enemyAttack.attackscore;
                        if (tempScore > randomValue)
                        {
                            currentAttack = enemyAttack;
                        }
                    }
                }
            }
        }*/
        #endregion
        }
    }
}
