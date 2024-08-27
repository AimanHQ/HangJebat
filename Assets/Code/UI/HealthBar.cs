using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HQ
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;

        private void Start()
        {
            slider = GetComponent<Slider>();
        }

        public void SetMaxHealth(int MaxHealth)
        {
            slider.maxValue = MaxHealth;
            slider.value = MaxHealth;
        }
        public void SetCurrentHealth(int currenthealth)
        {
            slider.value = currenthealth;
        }
    }
}
