using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetMethod
{
    Nearest,
    LowestHealth,
    HighestHealth
}

public class EnemyTargeting : MonoBehaviour
{
    public float targetingRange = 5.0f;
    public LayerMask targetLayer;
    public TargetMethod targetMethod = TargetMethod.Nearest;
    public bool targetLock = true;
    GameObject targetObject;

    // Update is called once per frame
    void Update()
    {
        if (targetObject != null)
        {
            CheckIfTargetIsInRange();
        }

        if (!targetLock || targetObject == null)
        {
            GetComponent<ProjectileLauncher>().canFire = false;
            FindNewTarget();
        }

        GetComponentInChildren<TowerLook>().lookTarget = targetObject != null ? targetObject.transform : null;

        if (targetObject != null)
        {
            GetComponent<ProjectileLauncher>().canFire = true;
        }
    }

    void CheckIfTargetIsInRange()
    {
        if ((targetObject.transform.position - transform.position).sqrMagnitude > targetingRange * targetingRange)
        {
            targetObject = null;
        }
    }

    void FindNewTarget()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, targetingRange, targetLayer);

        if (enemiesInRange.Length == 0)
        {
            return;
        }

        Collider highestPriorityTarget = enemiesInRange[0];

        for (int i = 1; i < enemiesInRange.Length; i++)
        {
            Collider currentEnemy = enemiesInRange[i];

            switch (targetMethod)
            {
                case TargetMethod.Nearest:
                    float sqrDistToHighestPriority = (highestPriorityTarget.transform.position - transform.position).sqrMagnitude;
                    float sqrDistToCurrentEnemy = (currentEnemy.transform.position - transform.position).sqrMagnitude;
                    if (sqrDistToCurrentEnemy < sqrDistToHighestPriority)
                    {
                        highestPriorityTarget = currentEnemy;
                    }
                    break;
                case TargetMethod.LowestHealth:
                    int lowestEnemyHealth = highestPriorityTarget.gameObject.GetComponent<HealthController>().currentHealth;
                    if (currentEnemy.gameObject.GetComponent<HealthController>().currentHealth < lowestEnemyHealth)
                    {
                        highestPriorityTarget = currentEnemy;
                    }
                    break;
                case TargetMethod.HighestHealth:
                    int highestEnemyHealth = highestPriorityTarget.gameObject.GetComponent<HealthController>().currentHealth;
                    if (currentEnemy.gameObject.GetComponent<HealthController>().currentHealth > highestEnemyHealth)
                    {
                        highestPriorityTarget = currentEnemy;
                    }
                    break;
            }
        }

        targetObject = highestPriorityTarget.gameObject;
    }
}
