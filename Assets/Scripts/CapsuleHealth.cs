using System;
using UnityEngine;

public class CapsuleHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    // Event that will be invoked when the capsule is destroyed
    public static event Action<int> OnCapsuleDestroyed;

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health
    }

    // Method to handle damage taken by the capsule
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Reduce health by the damage amount
        Debug.Log("Enemy health: " + currentHealth); // Debug log to show current health

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

        OnCapsuleDestroyed?.Invoke(10); // Deposit 10 coins per destroyed capsule
    }
}