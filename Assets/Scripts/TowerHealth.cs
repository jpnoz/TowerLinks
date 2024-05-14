using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    public int maxHealth = 200; // Starting health of the tower
    private int currentHealth; // Current health of the tower

    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health to starting health
    }

    // Property to expose currentHealth for external access
    public int CurrentHealth
    {
        get { return currentHealth; } // Getter method to retrieve current health
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
        int healAmount = Mathf.CeilToInt(amount); // Round up 'amount' to the nearest integer
        currentHealth += healAmount; // Add the rounded heal amount to current health
        currentHealth = Mathf.Min(currentHealth, maxHealth); // Clamp current health to starting health
        Debug.Log($"Default Tower healed for {healAmount}. Current health: {currentHealth}");
    }

    // Method to handle tower destruction
    private void Die()
    {
        Debug.Log("Default Tower Destroyed");
        Destroy(gameObject); // Destroy the tower GameObject
    }
}
