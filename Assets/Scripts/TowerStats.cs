using UnityEngine;

public struct StatBoost
{
    public StatBoost(float fireRateBoost = 0.0f, float maxHealthBoost = 0.0f, float attackDamageBoost = 0.0f, float fireRateMultiplier = 0.0f, float maxHealthMultiplier = 0.0f, float attackDamageMultiplier = 0.0f)
    {
        this.fireRateBoost = fireRateBoost;
        this.maxHealthBoost = maxHealthBoost;
        this.attackDamageBoost = attackDamageBoost;

        this.fireRateMultiplier = fireRateMultiplier;
        this.maxHealthMultiplier = maxHealthMultiplier;
        this.attackDamageMultiplier = attackDamageMultiplier;
    }

    public float fireRateBoost;
    public float maxHealthBoost;
    public float attackDamageBoost;

    public float fireRateMultiplier;
    public float maxHealthMultiplier;
    public float attackDamageMultiplier;
}

public class TowerStats : MonoBehaviour
{
    public int maxHealth = 200;
    public float fireRate = 1.0f;
    public float attackDamage = 50.0f;
    public int targetPriority = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
