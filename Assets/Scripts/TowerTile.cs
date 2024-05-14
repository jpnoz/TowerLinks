using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTile : MonoBehaviour
{
    public Transform towerSocket;
    public bool hasTower = false;
    GameObject placedTower;

    void Update()
    {
        if (placedTower == null)
        {
            hasTower = false;
        }
    }

    public void PlaceTower(GameObject towerToPlace)
    {
        hasTower = true;
        placedTower = GameObject.Instantiate(towerToPlace, towerSocket.position, Quaternion.identity);
    }
}
