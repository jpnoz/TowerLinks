using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultTower : MonoBehaviour
{
    public int maxHealth = 200; 
    private int currentHealth; 

    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health to starting health
    }

    // Method to handle damage taken by the tower
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Reduce health by the damage amount
        Debug.Log($"Tower took {damageAmount} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die(); // If health is zero or below, destroy the tower
        }
    }

    // Method to handle healing of the tower
    public void Heal(float amount)
    {
        currentHealth += (int)amount; // Explicitly cast 'amount' to 'int' before adding to 'currentHealth'
        currentHealth = Mathf.Min(currentHealth, maxHealth); // Clamp current health to starting health
        Debug.Log($"Tower healed for {amount}. Current health: {currentHealth}");
    }

    // Method to handle tower destruction
    private void Die()
    {
        Debug.Log("Tower Destroyed");
        Destroy(gameObject); // Destroy the tower GameObject
    }
}
