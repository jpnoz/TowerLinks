using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public string targetTag = "Enemy";
    public int damage = 25; // Amount of damage the cannonball deals

    private void Update()
    {
        if (transform.position.y < -10)
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the cannonball collides with a capsule
        if (other.CompareTag(targetTag))
        {
            CapsuleHealth capsuleHealth = other.GetComponent<CapsuleHealth>(); // Get the CapsuleHealth component

            if (capsuleHealth != null)
            {
                capsuleHealth.TakeDamage(damage); // Apply damage to the capsule
            }

            Destroy(gameObject); // Destroy the cannonball
        }
    }
}