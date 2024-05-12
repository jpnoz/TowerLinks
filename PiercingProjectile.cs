using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingProjectile : MonoBehaviour
{
    public int baseDamage = 25; 
    public int maxPierces = 3; 
    private int currentPierces = 0;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Projectile OnTriggerEnter: " + other.gameObject.name);

        // Check if the projectile collides with an enemy
        if (other.CompareTag("Enemy"))
        {
            CapsuleHealth capsuleHealth = other.GetComponent<CapsuleHealth>(); // Get the CapsuleHealth component

            if (capsuleHealth != null)
            {
                int adjustedDamage = CalculateAdjustedDamage(); // Calculate adjusted damage
                capsuleHealth.TakeDamage(adjustedDamage); // Apply adjusted damage to the enemy
            }

            currentPierces++; 

            // Check if the projectile has reached max pierces
            if (currentPierces >= maxPierces)
            {
                Destroy(gameObject); // Destroy the projectile if it reached max pierces
            }
        }
    }

    private int CalculateAdjustedDamage()
    {
        // Calculate the damage multiplier based on number of pierces
        float damageMultiplier = 1.0f - (currentPierces * 0.5f); // Halve damage for each pierce
        int adjustedDamage = Mathf.RoundToInt(baseDamage * damageMultiplier); // Apply the multiplier

        return adjustedDamage;
    }
}
