using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class AttackStates : State
    {
        public CombatStanceState combatStanceState;
        public EnemyAttack[] enemyattacks;
        public EnemyAttack currentAttack;
        bool isComboing = false;
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorHandler enemyAnimatorHandler)
        {
            //select of of many attack based on attack score
            //if select attack not used bcs bad angle or distance , select new attack
            //if attack viable, stop movement and attack target
            //set recovery time  to attack recovery time 
            //return combat state
            Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);


            if (enemyManager.isPerfomingAction && isComboing ==false)
            {
                return combatStanceState;
            }
            else if (isComboing)
            {
                enemyAnimatorHandler.PlayTargetAnimation(currentAttack.actionAnimation, true);
                isComboing = false; 
            }
            
            if (currentAttack != null)
            {
                //if close to enemy to perform current attack , get new attack
                if (distanceFromTarget < currentAttack.minimumDistanceNeedToAttack)
                {
                    return this;
                }
                //if close enough to attack, let proceed
                else if (distanceFromTarget < currentAttack.maximumDistanceNeedToAttack)
                {
                    //if our ebnemy is within our attack viewable angle, we attack 
                    if (viewableAngle <= currentAttack.maximumAttackAngle && viewableAngle 
                        >= currentAttack.minimumAttackAngle)
                    {
                        if (enemyManager.currentRecoveryTime <= 0 && enemyManager.isPerfomingAction == false)
                        {
                            enemyAnimatorHandler.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                            enemyAnimatorHandler.anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
                            enemyAnimatorHandler.PlayTargetAnimation(currentAttack.actionAnimation, true);
                            enemyAnimatorHandler.PlayWeaponTrailFX();
                            enemyManager.isPerfomingAction = true;

                            if (currentAttack.canCombo)
                            {
                                currentAttack = currentAttack.comboAction;
                                return this;
                            }
                            else
                            {
                            enemyManager.currentRecoveryTime = currentAttack.recoverytime;
                            currentAttack = null;
                            return combatStanceState;                                
                            }
                        }
                    }
                }
            }
            else 
            {
                GetNewAttack(enemyManager);
            }
            
            return combatStanceState;
        }

        private void GetNewAttack(EnemyManager enemyManager)
        {
            Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);

            int maxScore = 0;

            for (int i = 0; i < enemyattacks.Length; i++)
            {
                EnemyAttack enemyAttack = enemyattacks[i];
                if (distanceFromTarget <= enemyAttack.maximumDistanceNeedToAttack && distanceFromTarget
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
                if (distanceFromTarget <= enemyAttack.maximumDistanceNeedToAttack && distanceFromTarget
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
        }
    }
}
