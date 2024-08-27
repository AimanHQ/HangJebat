using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class EnemyManager : MonoBehaviour
    {
        Enemy enemylocomotion;
        bool isPerfomingAction;
        [Header ("AI SETTING")]
        //higher/lower angle are, greater the enemy fov is to detect you
        public float maximumDetectionAngle = 50;
        public float MinimumDetectionAngle = -50;
        public float detectionRadius = 20;
        private void Awake()
        {
            enemylocomotion = GetComponent<Enemy>();
        }
        
        private void Update()
        {
            HandleCurrentAction();
        }

        private void HandleCurrentAction()
        {
            if ( enemylocomotion.currentTarget == null)
            {
                enemylocomotion.HandleDetection();
            }
        }
    }
}
