using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class PlayerStats : Characterstats
    {
        public HealthBar healthBar;
        public StaminaBar staminaBar;
        public ManaBar manaBar;
        public float staminaRegenRate = 0.5f; // Amount of stamina to regenerate per second


        AnimatorHandler animatorHandler;

        private void Awake()
        {
            healthBar = FindObjectOfType<HealthBar>();
            staminaBar = FindObjectOfType<StaminaBar>();
            manaBar = FindObjectOfType<ManaBar>();
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

            MaxManalevel = SetMaxManaFromManaLevel();
            currentMana = MaxManalevel;
            manaBar.SetMaxMana(MaxManalevel);
            manaBar.SetCurrentMana(currentMana);
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

        private int SetMaxManaFromManaLevel()
        {
            MaxManalevel = ManaLevel * 10;
            return MaxManalevel;
        }

        public void TakeDamage(int damage)
        {
            if (isDead)
                return;


            currenthealth = currenthealth - damage;

            healthBar.SetCurrentHealth(currenthealth);
            animatorHandler.PlayTargetAnimation("Injured Walk Backwards", true);

            if (currenthealth <= 0) {
                currenthealth = 0;
                animatorHandler.PlayTargetAnimation("Dying Backwards", true);
                isDead = true;
                //handle player death
            }
        }
        public void TakeStaminaDamage(int damage)
        {
            currentstamina = currentstamina - damage;
            staminaBar.SetCurrentStamina(currentstamina);
        }

        public void healPlayer(int healAmount)
        {
            currenthealth = currenthealth + healAmount;

            if (currenthealth  > MaxHealth)
            {
                currenthealth = MaxHealth;
            }

            healthBar.SetCurrentHealth(currenthealth);
        }

        public void DeductMana(int mana)
        {   
            currentMana = currentMana - mana;

            if (currentMana < 0)
            {
                currentMana = 0;
            }
            manaBar.SetCurrentMana(currentMana);
        }

        public void RegenMana(int staminaRegenRate)
        {
            currentstamina = currentstamina + staminaRegenRate;

            if (currentstamina > maxStamina)
            {
                currentstamina = maxStamina;
            }

            staminaBar.SetCurrentStamina(currentstamina);
        }
    }
}
