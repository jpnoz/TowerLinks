using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    AttackTower,
    HealingTower,
    BoostTower,
}

public class TowerData : MonoBehaviour
{
    public TowerType towerType;
    public int maxConnections = 3;
    public List<GameObject> connectedTowers;
    public StatBoost outgoingStatBoost;
    public StatBoost incomingStatBoost;

    // Start is called before the first frame update
    void Start()
    {
        connectedTowers = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
