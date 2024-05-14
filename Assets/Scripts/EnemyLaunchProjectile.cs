using System.Collections;
using UnityEngine;

public class EnemyLaunchProjectile : MonoBehaviour
{
    public GameObject projectilePrefab; // The projectile GameObject to be launched
    public float launchVelocity = 10f; // The velocity at which the projectile is launched
    public float fireCooldown = 2f; // Cooldown between enemy shots
    public int damageAmount = 5; // Damage amount dealt by the enemy projectile

    private Transform target; // Reference to the tower (or target) Transform
    private float cooldownTimer = 0f; // Timer to track cooldown

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Tower")?.transform; // Find the tower's transform
    }

    private void Update()
    {
        if (target == null)
            return;

        if (cooldownTimer <= 0f)
        {
            LaunchProjectile(); // Launch projectile towards the tower
            cooldownTimer = fireCooldown; // Reset cooldown timer
        }

        cooldownTimer -= Time.deltaTime; // Update cooldown timer
    }

    private void LaunchProjectile()
    {
        if (projectilePrefab == null || target == null)
            return;

        // Calculate direction towards the tower
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        // Instantiate the projectile at the enemy's position with the calculated direction
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Set projectile damage
        ProjectileDamage projectileDamage = newProjectile.GetComponent<ProjectileDamage>();
        if (projectileDamage != null)
        {
            projectileDamage.SetDamage(damageAmount);
        }

        // Rotate the projectile to face towards the tower along the z-axis
        Vector3 newForward = Vector3.ProjectOnPlane(directionToTarget, Vector3.up).normalized; // Project onto the horizontal plane
        newProjectile.transform.forward = newForward; // Set projectile's forward direction

        // Calculate velocity for the projectile
        Rigidbody projectileRigidbody = newProjectile.GetComponent<Rigidbody>();
        if (projectileRigidbody != null)
        {
            projectileRigidbody.velocity = directionToTarget * launchVelocity;
        }
    }
}
