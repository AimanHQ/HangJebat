using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class Enemy : MonoBehaviour
    {
        EnemyManager enemyManager;
        EnemyAnimatorHandler enemyAnimatorHandler;

        private  void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
        }
    }
}
