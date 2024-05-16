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
    public GameObject connectionLineRenderer;
    public TowerStats towerStats;
    public int maxConnections = 3;
    public List<TowerData> connectedTowers;
    StatBoost currentStatBoost;
    bool preparetoupdate = false;

    private void OnEnable()
    {
        //TowerHealth.OnTowerDestroyed += PrepareToUpdateConnections;
    }

    private void OnDisable()
    {
        //TowerHealth.OnTowerDestroyed -= PrepareToUpdateConnections;
    }

    // Start is called before the first frame update
    void Start()
    {
        connectedTowers = new List<TowerData>();
        towerStats = GetComponent<TowerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (preparetoupdate)
        {
            UpdateConnections();
            preparetoupdate = false;
        }
        transmitStats();
    }

    // Needed as a buffer so connections are updated on frame
    // after a tower is destroyed
    void PrepareToUpdateConnections()
    {
        preparetoupdate = true;
    }

    void UpdateConnections()
    {
        for (int i = 0; i < connectedTowers.Count; i++)
        {
            TowerData towerData = connectedTowers[i];
            if (towerData == null)
            {
                connectedTowers.Remove(towerData);
                i--;
            }
        }

        // Delete all Line Renderers
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<LineRenderer>())
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        // Connect Line Renderers and Calculate Stat Boosts
        currentStatBoost = new StatBoost();
        foreach (TowerData towerData in connectedTowers)
        {
            ConnectLineToTower(towerData.transform);
            currentStatBoost += TowerStats.getStatBoost(towerType, towerData.towerType);
        }

        towerStats.applyStatBoost(currentStatBoost);
        transmitStats();
    }

    public void ConnectTower(TowerData connectingTowerData)
    {
        connectedTowers.Add(connectingTowerData);

        UpdateConnections();
    }

    void ConnectLineToTower(Transform tower)
    {
        GameObject newLine = GameObject.Instantiate(connectionLineRenderer, transform.position, Quaternion.identity);
        newLine.transform.parent = transform;
        
        LineRenderer newLineRenderer = newLine.GetComponent<LineRenderer>();
        newLineRenderer.SetPosition(0, Vector3.zero);

        // Calculate vector between towers
        // LineRenderer uses local Co-ords
        Vector3 towerLink = tower.position - transform.position;
        newLineRenderer.SetPosition(1, towerLink);

    }

    void transmitStats()
    {
        
    }
}
