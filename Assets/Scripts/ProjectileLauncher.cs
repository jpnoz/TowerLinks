using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public Transform launchOrigin;
    public GameObject projectile;

    public bool canFire = false;
    public float fireRate = 1;
    float currentFireCooldown;

    // Start is called before the first frame update
    void Start()
    {
        currentFireCooldown = 1 / fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canFire)
        {
            currentFireCooldown = 1 / fireRate;
            return;
        }

        currentFireCooldown -= Time.deltaTime;
        if (currentFireCooldown <= 0.0f)
        {
            LaunchProjectile();
            currentFireCooldown = 1 / fireRate;
        }
    }

    public void LaunchProjectile()
    {
        Instantiate(projectile, launchOrigin.position, launchOrigin.rotation);
    }
}
