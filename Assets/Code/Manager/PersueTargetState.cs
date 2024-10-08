using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class PersueTargetState : State
    {
        public CombatStanceState  combatStanceState;
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorHandler enemyAnimatorHandler)
        {
            //chase target
            //if within range , switch combat stance
            //if target out range , return state and chase target
            if (enemyManager.isPerfomingAction)
            {
                enemyAnimatorHandler.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                return this;   
            }

            Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

            if (distanceFromTarget > enemyManager.maximumAttackRange)
            {
                enemyAnimatorHandler.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            }


            HandleRotateTowardTarget(enemyManager);

            // Update the enemy's position to match the NavMeshAgent's Y position
            Vector3 enemyPosition = enemyManager.transform.position;
            enemyPosition.y = enemyManager.NavMeshAgent.transform.position.y;
            enemyManager.transform.position = enemyPosition;


            if (distanceFromTarget <= enemyManager.maximumAttackRange)
            {
                return combatStanceState;
            }
            else 
            {
                return this;
            }
        }

        public  void HandleRotateTowardTarget(EnemyManager enemyManager)
        {
            //rotate manual
            if (enemyManager.isPerfomingAction)
            {
                Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }

                Quaternion targetrotation = Quaternion.LookRotation(direction);
                enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, enemyManager.rotationspeed / Time.deltaTime);
            }
            //rotate with pathfinding navmesh
            else 
            {
            enemyManager.NavMeshAgent.enabled = true;
            enemyManager.NavMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

            float rotationToApplyToDynamicEnemy = Quaternion.Angle(enemyManager.transform.rotation, Quaternion.LookRotation(enemyManager.NavMeshAgent.desiredVelocity.normalized));
            if (distanceFromTarget > 5) enemyManager.NavMeshAgent.angularSpeed = 500f;
            else if (distanceFromTarget < 5 && Mathf.Abs(rotationToApplyToDynamicEnemy) < 30) enemyManager.NavMeshAgent.angularSpeed = 50f;
            else if (distanceFromTarget < 5 && Mathf.Abs(rotationToApplyToDynamicEnemy) > 30) enemyManager.NavMeshAgent.angularSpeed = 500f;

            Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            Quaternion rotationToApplyToStaticEnemy = Quaternion.LookRotation(targetDirection);


            if (enemyManager.NavMeshAgent.desiredVelocity.magnitude > 0)
            {
                enemyManager.NavMeshAgent.updateRotation = false;
                enemyManager.transform.rotation = Quaternion.RotateTowards(enemyManager.transform.rotation,
                Quaternion.LookRotation(enemyManager.NavMeshAgent.desiredVelocity.normalized), enemyManager.NavMeshAgent.angularSpeed * Time.deltaTime);
            }
            else
            {
                enemyManager.transform.rotation = Quaternion.RotateTowards(enemyManager.transform.rotation, rotationToApplyToStaticEnemy, enemyManager.NavMeshAgent.angularSpeed * Time.deltaTime);
            }
            }
        }
    }
}
