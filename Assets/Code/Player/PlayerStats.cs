using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class PlayerStats : Characterstats
    {
        public HealthBar healthBar;
        public StaminaBar staminaBar;
        

        AnimatorHandler animatorHandler;

        private void Awake()
        {
            healthBar = FindObjectOfType<HealthBar>();
            staminaBar = FindObjectOfType<StaminaBar>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }
        void Start()
        {
            MaxHealth = SetMaxHealthFromHealthLevel();
            currenthealth = MaxHealth;
            healthBar.SetMaxHealth(MaxHealth);

            maxStamina = SetMaxHealthFromHealthLevel();
            currentstamina = maxStamina;
            staminaBar.SetMaxStamina(maxStamina);
        }

        private int SetMaxHealthFromHealthLevel()
        {
            MaxHealth = healthlevel * 10;
            return MaxHealth;
        }


        private int SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminalevel * 10;
            return maxStamina;
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
        public void TakeStaminaDamage(int damage)
        {
            currentstamina = currentstamina - damage;
            staminaBar.SetCurrentStamina(currentstamina);
        }
    }
}
