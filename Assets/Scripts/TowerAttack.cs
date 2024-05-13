using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    public GameObject projectile;
    public Transform towerHead;
    public Transform launchOrigin;
    public float launchVelocity = 100f; 
    public float range = 10f; 
    public float fireCooldown = 0.2f; 
    private float cooldownTimer = 0.0f; 
    [SerializeField] private string targetTag = "Enemy"; 

    private void Update()
    {
        // If cooldown is over, find and fire at the target
        if (cooldownTimer <= 0.0f)
        {
            FindAndAttackTarget();
            cooldownTimer = fireCooldown; // Reset cooldown timer
        }

        if (cooldownTimer > 0.0f)
        {
            cooldownTimer -= Time.deltaTime; // Update cooldown timer
        }
    }

    // Method to find and attack the target
    private void FindAndAttackTarget()
    {
        // Find all GameObjects with the specified tag
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        GameObject nearestTarget = null;
        float nearestDistance = Mathf.Infinity;

        // Find the nearest target within range
        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance <= range && distance < nearestDistance)
            {
                nearestTarget = target;
                nearestDistance = distance;
            }
        }

        // If a valid target is found, launch projectile at it
        if (nearestTarget != null)
        {
            LaunchProjectileAt(nearestTarget.transform);
        }
    }

    // Method to launch the projectile towards the target
    private void LaunchProjectileAt(Transform target)
    {
        float aimVerticalOffset = Mathf.Abs(towerHead.position.y - launchOrigin.position.y);

        // Calculate the direction towards the target
        Vector3 directionToTarget = (target.position - towerHead.position);
        directionToTarget.y -= aimVerticalOffset;
        directionToTarget.Normalize();

        // Rotate the tower's launch origin to face towards the target
        towerHead.rotation = Quaternion.LookRotation(directionToTarget);

        // Instantiate the projectile at the tower's position with tower's rotation
        GameObject newProjectile = Instantiate(projectile, launchOrigin.position, towerHead.rotation);

        // Add force to the projectile in the adjusted direction
        newProjectile.GetComponent<Rigidbody>().AddForce(directionToTarget * launchVelocity);
    }
}
