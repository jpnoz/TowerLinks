using UnityEngine;

public class EnemyGoal : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public float enemyDetectionRange = 0.6f;

    public delegate void EnemyGoalDelegate();
    public static event EnemyGoalDelegate OnEnemyGoal;

    // Update is called once per frame
    void Update()
    {
        // Check if Enemy Reached Goal
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit) && hit.transform.CompareTag(enemyTag))
        {
            Vector3 enemyToGoal = hit.transform.position - transform.position;

            if (enemyToGoal.sqrMagnitude <= enemyDetectionRange * enemyDetectionRange)
            {
                // Destroy Enemy GameObject and Invoke Event
                DestroyImmediate(hit.transform.gameObject);

                Debug.Log("Enemy Reached Goal");
                OnEnemyGoal?.Invoke();
            }
        }
    }
}
