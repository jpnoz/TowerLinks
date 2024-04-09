using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{
    public GameObject projectile; // The projectile GameObject to be launched
    public float launchVelocity = 100f; // The velocity at which the projectile is launched
    public GameObject enemy; // Reference to the enemy GameObject
    private bool continuousFire = true; // Flag to control continuous firing
    public float fireCooldown = 0.2f; // Cooldown between successive shots
    private float cooldownTimer = 0.0f; // Timer to track cooldown


    // Start is called before the first frame update
    void Start()
    {
        // Check if the enemy exists and is within range before attacking
        if (enemy != null && IsEnemyInRange())
        {
            StartCoroutine(FireContinuous()); // Start firing continuously
        }
    }

    // Coroutine to continuously fire projectiles
    IEnumerator FireContinuous()
    {
        while (continuousFire) // Continue firing as long as continuousFire flag is true
        {
            if (IsEnemyInRange() && cooldownTimer <= 0.0f) // Check if the enemy is still within range and cooldown is over
            {
                LaunchProjectileAtEnemy(); // Launch projectile at the enemy
                cooldownTimer = fireCooldown; // Reset cooldown timer
            }

            yield return null; // Yield to allow other processes to execute

            if (cooldownTimer > 0.0f)
            {
                cooldownTimer -= Time.deltaTime; // Update cooldown timer
            }
        }
    }

    // Method to check if the enemy is within range
    bool IsEnemyInRange()
    {
        if (enemy == null) // If there's no enemy, return false
        {
            return false;
        }

        // Calculate the distance between the tower and the enemy
        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

        // Define the attack range; adjust this value based on your game's requirements
        float attackRange = 20f;

        // Return true if the enemy is within attack range, false otherwise
        return distanceToEnemy <= attackRange;
    }

    // Method to launch the projectile towards the enemy
    void LaunchProjectileAtEnemy()
    {
        // Instantiate the projectile at the tower's position with tower's rotation
        GameObject Cannonball = Instantiate(projectile, transform.position, transform.rotation);

        // Calculate the direction towards the enemy
        Vector3 directionToEnemy = (enemy.transform.position - transform.position).normalized;

        // Add an upward offset to the direction to aim higher on the enemy
        Vector3 adjustedDirection = directionToEnemy + Vector3.up * 0.2f; // Adjust the offset as needed

        // Add force to the projectile in the adjusted direction
        Cannonball.GetComponent<Rigidbody>().AddForce(adjustedDirection * launchVelocity);
    }

    // Method to stop continuous firing
    public void StopContinuousFire()
    {
        continuousFire = false; // Set continuousFire flag to false to stop firing
    }


}
