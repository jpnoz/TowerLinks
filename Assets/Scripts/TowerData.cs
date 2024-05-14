using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    AttackTower,
    HealingTower,
    BoostTower,
}

[RequireComponent(typeof(TowerStats))]
public class TowerData : MonoBehaviour
{
    public TowerType towerType;
    public int maxConnections = 3;
    public List<TowerData> connectedTowers;
    public TowerStats towerStats;
    StatBoost currentStatBoost;

    // Start is called before the first frame update
    void Start()
    {
        connectedTowers = new List<TowerData>();
        towerStats = GetComponent<TowerStats>();

        transmitStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectTower(TowerData connectingTowerData)
    {
        connectedTowers.Add(connectingTowerData);

        currentStatBoost = new StatBoost();
        foreach (TowerData towerData in connectedTowers)
        {
            currentStatBoost += TowerStats.getStatBoost(towerType, towerData.towerType);
        }

        towerStats.applyStatBoost(currentStatBoost);
        transmitStats();
    }

    void transmitStats()
    {
        TowerHealth towerHealth = GetComponent<TowerHealth>(); 
        if (!towerHealth)
        {
            return;
        }

        towerHealth.maxHealth = towerStats.currentMaxHealth;

        if (towerType == TowerType.AttackTower)
        {
            TowerAttack towerAttack = GetComponent<TowerAttack>();
            if (!towerAttack)
            {
                return;
            }

            towerAttack.fireCooldown = 1.0f / towerStats.currentFireRate;
            towerAttack.attackDamage = towerStats.currentFireValue;
        }

        if (towerType == TowerType.HealingTower)
        {
            HealingTower towerHealing = GetComponent<HealingTower>();
            if (!towerHealing)
            {
                return;
            }

            towerHealing.healInterval = 1.0f / towerStats.currentFireRate;
            towerHealing.healAmount = towerStats.currentFireValue;
        }
    }
}
