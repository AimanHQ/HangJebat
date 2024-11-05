using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HQ
{
    public class EnemyStats : Characterstats
    {
        public event Action OnDeath; // Instance-specific event for enemy death
        Animator animator;
        EnemyManager enemyManager;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            enemyManager = GetComponent<EnemyManager>();
        }
        void Start()
        {
            MaxHealth = SetMaxHealthFromHealthLevel();
            currenthealth = MaxHealth;
        }

        private int SetMaxHealthFromHealthLevel()
        {
            MaxHealth = healthlevel * 10;
            return MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (isDead)
                return;
            currenthealth = currenthealth - damage;

            animator.Play("Injured Walk Backwards");

            if (currenthealth <= 0) {
                currenthealth = 0;
                animator.Play("Dying Backwards");
                isDead = true;
                enemyManager.OnDeath();

                //handle enemy death


                // Notify that this enemy has died
                OnDeath?.Invoke();
            }
        }

    }
}
