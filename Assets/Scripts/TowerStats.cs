using UnityEngine;

public struct StatBoost
{
    public float fireRateBoost;
    public int maxHealthBoost;
    public int attackDamageBoost;
    public int targetPriorityBoost;

    public float fireRateMultiplier;
    public float maxHealthMultiplier;
    public float attackDamageMultiplier;

    public StatBoost(float fireRateBoost = 0.0f, int maxHealthBoost = 0, int attackDamageBoost = 0, int targetPriorityBoost = 0, float fireRateMultiplier = 0.0f, float maxHealthMultiplier = 0.0f, float attackDamageMultiplier = 0.0f)
    {
        this.fireRateBoost = fireRateBoost;
        this.maxHealthBoost = maxHealthBoost;
        this.attackDamageBoost = attackDamageBoost;
        this.targetPriorityBoost = targetPriorityBoost;

        this.fireRateMultiplier = fireRateMultiplier;
        this.maxHealthMultiplier = maxHealthMultiplier;
        this.attackDamageMultiplier = attackDamageMultiplier;
    }

    public static StatBoost operator +(StatBoost a, StatBoost b)
    {
        return new StatBoost(
                a.fireRateBoost + b.fireRateBoost,
                a.maxHealthBoost + b.maxHealthBoost,
                a.attackDamageBoost + b.attackDamageBoost,
                a.targetPriorityBoost + b.targetPriorityBoost,

                a.fireRateMultiplier + b.fireRateMultiplier,
                a.maxHealthMultiplier + b.maxHealthMultiplier,
                a.attackDamageMultiplier + b.attackDamageMultiplier
            );
    }
}

public class TowerStats : MonoBehaviour
{
    [SerializeField] int baseMaxHealth = 200;
    [SerializeField] float baseFireRate = 1.0f;
    [SerializeField] float baseAttackDamage = 50.0f;
    [SerializeField] int baseTargetPriority = 1;

    public int currentMaxHealth;
    public float currentFireRate;
    public float currentAttackDamage;
    public int currentTargetPriority;

    // Start is called before the first frame update
    void Start()
    {
        currentMaxHealth = baseMaxHealth;
        currentFireRate = baseFireRate;
        currentAttackDamage = baseAttackDamage;
        currentTargetPriority = baseTargetPriority;
    }

    public void applyStatBoost(StatBoost statBoosts)
    {
        currentMaxHealth = (int)(baseMaxHealth * (1 + statBoosts.maxHealthMultiplier)) + statBoosts.maxHealthBoost;
        currentFireRate = (baseFireRate * (1 + statBoosts.fireRateMultiplier)) + statBoosts.fireRateBoost;
        currentAttackDamage = (int)(baseAttackDamage * (1 + statBoosts.attackDamageMultiplier)) + statBoosts.attackDamageBoost;
        currentTargetPriority = baseTargetPriority + statBoosts.targetPriorityBoost;
    }

    public static StatBoost getStatBoost(TowerType connectingTowerType, TowerType targetTowerType)
    {
        switch (connectingTowerType)
        {
            case TowerType.AttackTower:
                return getAttackTowerBoost(targetTowerType);
            case TowerType.HealingTower:
                return getHealingTowerBoost(targetTowerType);
            case TowerType.BoostTower:
                return getBoostTowerBoost(targetTowerType);
            default:
                return new StatBoost();
        }
    }

    public static StatBoost getAttackTowerBoost(TowerType targetTowerType)
    {
        switch (targetTowerType)
        {
            case TowerType.AttackTower:
                return new StatBoost(fireRateBoost: 2.0f);
            case TowerType.HealingTower:
                return new StatBoost(maxHealthBoost: 30, targetPriorityBoost: 2);
            case TowerType.BoostTower:
                return new StatBoost(attackDamageBoost: 10);
            default:
                return new StatBoost();
        }
    }

    public static StatBoost getHealingTowerBoost(TowerType targetTowerType)
    {
        switch (targetTowerType)
        {
            case TowerType.AttackTower:
                return new StatBoost(maxHealthBoost: -30, targetPriorityBoost: -1);
            case TowerType.HealingTower:
                return new StatBoost(fireRateBoost: 3.0f);
            case TowerType.BoostTower:
                return new StatBoost(maxHealthBoost: 40);
            default:
                return new StatBoost();
        }
    }

    public static StatBoost getBoostTowerBoost(TowerType targetTowerType)
    {
        switch (targetTowerType)
        {
            case TowerType.AttackTower:
                return new StatBoost(targetPriorityBoost: 3);
            case TowerType.HealingTower:
                return new StatBoost(targetPriorityBoost: -1);
            case TowerType.BoostTower:
                return new StatBoost(targetPriorityBoost: -2);
            default:
                return new StatBoost();
        }
    }
}
