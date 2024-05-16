using UnityEngine;

public struct StatBoost
{
    public float fireRateBoost;
    public int maxHealthBoost;
    public int fireValueBoost;
    public int targetPriorityBoost;

    public float fireRateMultiplier;
    public float maxHealthMultiplier;
    public float fireValueMultiplier;

    public StatBoost(float fireRateBoost = 0.0f, int maxHealthBoost = 0, int fireValueBoost = 0, int targetPriorityBoost = 0, float fireRateMultiplier = 0.0f, float maxHealthMultiplier = 0.0f, float fireValueMultiplier = 0.0f)
    {
        this.fireRateBoost = fireRateBoost;
        this.maxHealthBoost = maxHealthBoost;
        this.fireValueBoost = fireValueBoost;
        this.targetPriorityBoost = targetPriorityBoost;

        this.fireRateMultiplier = fireRateMultiplier;
        this.maxHealthMultiplier = maxHealthMultiplier;
        this.fireValueMultiplier = fireValueMultiplier;
    }

    public static StatBoost operator +(StatBoost a, StatBoost b)
    {
        return new StatBoost(
            a.fireRateBoost + b.fireRateBoost,
            a.maxHealthBoost + b.maxHealthBoost,
            a.fireValueBoost + b.fireValueBoost,
            a.targetPriorityBoost + b.targetPriorityBoost,

            a.fireRateMultiplier + b.fireRateMultiplier,
            a.maxHealthMultiplier + b.maxHealthMultiplier,
            a.fireValueMultiplier + b.fireValueMultiplier
        );
    }
}

public class TowerStats : MonoBehaviour
{
    [SerializeField] int baseMaxHealth = 200;
    [SerializeField] float baseFireRate = 1.0f;
    [SerializeField] int baseFireValue = 50;
    [SerializeField] int baseTargetPriority = 1;

    public int currentMaxHealth;
    public float currentFireRate;
    public int currentFireValue;
    public int currentTargetPriority;

    // Start is called before the first frame update
    void Start()
    {
        currentMaxHealth = baseMaxHealth;
        currentFireRate = baseFireRate;
        currentFireValue = baseFireValue;
        currentTargetPriority = baseTargetPriority;
    }

    public void applyStatBoost(StatBoost statBoosts)
    {
        currentMaxHealth = (int)(baseMaxHealth * (1 + statBoosts.maxHealthMultiplier)) + statBoosts.maxHealthBoost;
        currentFireRate = (baseFireRate * (1 + statBoosts.fireRateMultiplier)) + statBoosts.fireRateBoost;
        currentFireValue = (int)(baseFireValue * (1 + statBoosts.fireValueMultiplier)) + statBoosts.fireValueBoost;
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
                return new StatBoost(fireValueBoost: 10);
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
