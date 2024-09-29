using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class EnemyAnimatorHandler : AnimatorManager
    {
        EnemyManager enemyManager;
        EnemyFxManager enemyFxManager;
        private void Awake()
        {
            anim = GetComponent<Animator>();
            enemyManager = GetComponentInParent<EnemyManager>();
            enemyFxManager = GetComponent<EnemyFxManager>();
        }

        public void PlayWeaponTrailFX()
        {
            enemyFxManager.PlayWeaponFx(false); 
        }
        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            enemyManager.enemyRB.drag = 0;
            Vector3 deltaposition = anim.deltaPosition;
            deltaposition.y = 0;
            Vector3 velocity = deltaposition / delta;
            enemyManager.enemyRB.velocity = velocity ;
        }
    }
}
