using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    private int damageAmount; // Damage amount to be dealt

    // Method to set the damage amount for the projectile
    public void SetDamage(int damage)
    {
        damageAmount = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the projectile collides with the tower
        if (other.CompareTag("Tower"))
        {
            TowerHealth towerHealth = other.GetComponent<TowerHealth>(); // Get the TowerHealth component

            if (towerHealth != null)
            {
                towerHealth.TakeDamage(damageAmount); // Apply damage to the tower
            }

            Destroy(gameObject); // Destroy the projectile
        }
    }
}

