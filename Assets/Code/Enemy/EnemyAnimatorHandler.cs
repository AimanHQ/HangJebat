using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class EnemyAnimatorHandler : AnimatorManager
    {
        Enemy enemylocmotion;
        private void Awake()
        {
            anim = GetComponent<Animator>();
            enemylocmotion = GetComponentInParent<Enemy>();
        }

        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            enemylocmotion.enemyRB.drag = 0;
            Vector3 deltaposition = anim.deltaPosition;
            deltaposition.y = 0;
            Vector3 velocity = deltaposition / delta;
            enemylocmotion.enemyRB.velocity = velocity ;
        }
    }
}
