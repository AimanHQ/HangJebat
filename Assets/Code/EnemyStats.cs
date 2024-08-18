using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class EnemyStats : MonoBehaviour
    {
        public int healthlevel = 10;
        public int MaxHealth;
        public int currenthealth;

        Animator animator;
        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
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
            currenthealth = currenthealth - damage;

            animator.Play("Injured Walk Backwards");

            if (currenthealth <= 0) {
                currenthealth = 0;
                animator.Play("Dying Backwards");
                //handle enemy death
            }
        }

    }
}
