using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class PlayerStats : MonoBehaviour
    {
        public int healthlevel = 10;
        public int MaxHealth;
        public int currenthealth;
        public HealthBar healthBar;

        AnimatorHandler animatorHandler;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }
        void Start()
        {
            MaxHealth = SetMaxHealthFromHealthLevel();
            currenthealth = MaxHealth;
            healthBar.SetMaxHealth(MaxHealth);
        }

        private int SetMaxHealthFromHealthLevel()
        {
            MaxHealth = healthlevel * 10;
            return MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            currenthealth = currenthealth - damage;

            healthBar.SetCurrentHealth(currenthealth);
            animatorHandler.PlayTargetAnimation("Injured Walk Backwards", true);

            if (currenthealth <= 0) {
                currenthealth = 0;
                animatorHandler.PlayTargetAnimation("Dying Backwards", true);
                //handle player death
            }
        }

    }
}
