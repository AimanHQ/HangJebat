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
                return this;

            Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
            enemyManager.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

            if (enemyManager.distanceFromTarget > enemyManager.maximumAttackRange)
            {
                enemyAnimatorHandler.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            }


            HandleRotateTowardTarget(enemyManager);
            //transform.position = new Vector3(transform.position.x, NavMeshAgent.transform.position.y, transform.position.z);
            enemyManager.NavMeshAgent.transform.localPosition = Vector3.zero;
            enemyManager.NavMeshAgent.transform.localRotation = Quaternion.identity;

            if (enemyManager.distanceFromTarget <= enemyManager.maximumAttackRange)
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
                Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.NavMeshAgent.desiredVelocity);
                Vector3 targetVelocity =enemyManager.enemyRB.velocity;

                enemyManager.NavMeshAgent.enabled = true;
                enemyManager.NavMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
                enemyManager.enemyRB.velocity = targetVelocity;
                enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, enemyManager.NavMeshAgent.transform.rotation, enemyManager.rotationspeed / Time.deltaTime);
            }
        }
    }
}
