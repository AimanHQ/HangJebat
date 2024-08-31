using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class Enemy : MonoBehaviour
    {
        EnemyManager enemyManager;
        EnemyAnimatorHandler enemyAnimatorHandler;
        public CapsuleCollider charactercollider;   
        public CapsuleCollider charactercollionblocker;

        private  void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
        }

        private void Start()
        {
            Physics.IgnoreCollision(charactercollider, charactercollionblocker, true);
        }
    }
}
