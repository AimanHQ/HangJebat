using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace HQ
{
    public class DamageCollider : MonoBehaviour
    {
        Collider damageCollider;
        public int currentweapondamage = 25;

        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = false;
        }    
        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Player")
            {
                PlayerStats playerStats = collision.GetComponent<PlayerStats>();

                if (playerStats != null) {
                    playerStats.TakeDamage(currentweapondamage);
                }
            }

            if (collision.tag == ("Enemy"))
            {
                EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

                if (enemyStats != null) {
                    enemyStats.TakeDamage(currentweapondamage);
                }
            }
        }
    }
}
