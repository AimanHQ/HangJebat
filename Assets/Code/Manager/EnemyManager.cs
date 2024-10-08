using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;

namespace HQ
{
    public class EnemyManager : MonoBehaviour
    {
        Enemy enemylocomotion;
        EnemyAnimatorHandler enemyAnimatorHandler;
        EnemyStats enemyStats;


        public State currentState;
        public Characterstats currentTarget;
        public NavMeshAgent NavMeshAgent;
        public Rigidbody enemyRB;

        public bool isPerfomingAction;
        public bool isInteracting;
        public float rotationspeed = 70;
        public float maximumAttackRange = 1.5f;

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
            enemyRB = GetComponent<Rigidbody>();
            NavMeshAgent = GetComponentInChildren<NavMeshAgent>();
            NavMeshAgent.enabled = false;
        }

        private void Start()
        {
            enemyRB.isKinematic = false;
        }
        
        private void Update()
        {
            HandleRecoveryTime();
            HandleStateMachine();

            isInteracting = enemyAnimatorHandler.anim.GetBool("IsInteract");
        }

        private void FixedUpdate()
        {
            // Keep the NavMeshAgent's local position at zero (to prevent it from affecting the Y-position)
            NavMeshAgent.transform.localPosition = Vector3.zero;
            NavMeshAgent.transform.localRotation = Quaternion.identity;
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
    }
}
