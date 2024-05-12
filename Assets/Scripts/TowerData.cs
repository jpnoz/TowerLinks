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
    }
}
