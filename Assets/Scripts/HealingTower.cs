using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingTower : MonoBehaviour
{
    public ParticleSystem healingParticles; // Reference to the Particle System component
    public float healRange = 10f; // Range within which towers will be healed
    public float healAmount = 5f; // Amount of health to heal per interval
    public float healInterval = 2f; // Interval between each healing action in seconds

    private float nextHealTime; // Time when the next healing action can occur

    private void Start()
    {
        // Initialize nextHealTime to start healing immediately
        nextHealTime = Time.time;
    }

    private void Update()
    {
        // Check if it's time to perform another healing action
        if (Time.time >= nextHealTime)
        {
            bool towersInRange = false; // Flag to track if towers are within range

            // Find all towers with the "DefaultTower" tag within the healRange
            GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");

            foreach (GameObject towerObj in towers)
            {
                // Calculate distance to the tower
                float distance = Vector3.Distance(transform.position, towerObj.transform.position);

                if (distance <= healRange)
                {
                    // Attempt to get the TowerHealth component of the tower
                    TowerHealth tower = towerObj.GetComponent<TowerHealth>();

                    if (tower != null)
                    {
                        // Heal the tower's health by the specified amount
                        tower.Heal(healAmount);

                        // Output debug information to the console using CurrentHealth property
                        Debug.Log($"Tower healed for {healAmount} health. Current health: {tower.CurrentHealth}");

                        towersInRange = true; // Set flag to true if at least one tower is in range
                    }
                }
            }

            // Control particle emission based on tower presence
            if (towersInRange)
            {
                // Start emitting particles if towers are within range
                if (!healingParticles.isPlaying)
                {
                    healingParticles.Play();
                }
            }
            else
            {
                // Stop emitting particles if no towers are within range
                if (healingParticles.isPlaying)
                {
                    healingParticles.Stop();
                }
            }

            // Update the next heal time based on the heal interval
            nextHealTime = Time.time + healInterval; // Set the next heal time after the interval
        }
    }
}
