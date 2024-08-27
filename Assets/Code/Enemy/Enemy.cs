using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class Enemy : MonoBehaviour
    {
        EnemyManager enemyManager;
        public Characterstat currentTarget;
        public LayerMask detectionLayer;
        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
        }
        public void HandleDetection()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                Characterstat characterstat = colliders[i].transform.GetComponent<Characterstat>();

                if (characterstat != null)
                {
                    //check for team id
                    Vector3 targetDirection = characterstat.transform.position - transform.position;
                    float viewableAngle =Vector3.Angle(targetDirection, transform.forward);

                    if (viewableAngle > enemyManager.MinimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                    {
                        currentTarget = characterstat;
                    }
                }
            }
        }
    }
}
