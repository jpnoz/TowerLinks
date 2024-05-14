using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class TowerTile : MonoBehaviour
{
    public Transform towerSocket;
    public bool hasTower = false;
    GameObject placedTower;

    public void PlaceTower(GameObject towerToPlace)
    {
        placedTower = towerToPlace;
        hasTower = true;
        GameObject.Instantiate(placedTower, towerSocket.position, Quaternion.identity);
    }
}
