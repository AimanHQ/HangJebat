using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HQ
{
    public class Enemy : MonoBehaviour
    {
        EnemyManager enemyManager;
        EnemyAnimatorHandler enemyAnimatorHandler;
        NavMeshAgent NavMeshAgent;
        public Rigidbody enemyRB;
        public LayerMask detectionLayer;
        public float distanceFromTarget;
        public float stoppingDistance = 1f;
        public float yOffset = 15;
        public float rotationspeed = 70;
        private  void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
            NavMeshAgent = GetComponentInChildren<NavMeshAgent>();
            enemyRB = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            NavMeshAgent.enabled = false;
            enemyRB.isKinematic = false;
        }
    
        public  void HandleMoveToTraget()
        {
            if (enemyManager.isPerfomingAction)
                return;
            Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
            distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

            //if perfom action , stop movement
            if (enemyManager.isPerfomingAction)
            {
                enemyAnimatorHandler.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                NavMeshAgent.enabled =false;
            }
            else 
            {
                if (distanceFromTarget > stoppingDistance)
                {
                    enemyAnimatorHandler.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
                }
                else if (distanceFromTarget <= stoppingDistance)
                {
                    enemyAnimatorHandler.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                }
            }
            HandleRotateTowardTarget();
            transform.position = new Vector3(transform.position.x, NavMeshAgent.transform.position.y, transform.position.z);
 
            NavMeshAgent.transform.localPosition = Vector3.zero;
            NavMeshAgent.transform.localRotation = Quaternion.identity;
    }

        public  void HandleRotateTowardTarget()
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
                transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, rotationspeed / Time.deltaTime);
            }
            //rotate with pathfinding navmesh
            else 
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(NavMeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyRB.velocity;

                NavMeshAgent.enabled = true;
                NavMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
                enemyRB.velocity = targetVelocity;
                transform.rotation = Quaternion.Slerp(transform.rotation, NavMeshAgent.transform.rotation, rotationspeed / Time.deltaTime);
            }
        }
    }
}
