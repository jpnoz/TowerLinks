using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the capsule
    private int currentHealth; // Current health of the capsule

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health
    }

    // Method to handle damage taken by the capsule
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Reduce health by the damage amount
        Debug.Log("enemy health: " + currentHealth); // Debug log to show current health


        if (currentHealth <= 0)
        {
            Die(); // If health is zero or below, destroy the capsule
        }
    }

    // Method to handle capsule destruction
    void Die()
    {
        Debug.Log("Enemy Destroyed"); // Debug log to indicate capsule destruction
        Destroy(gameObject); // Destroy the capsule GameObject
    }
}
