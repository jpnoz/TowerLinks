using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPlacement : MonoBehaviour
{
    public GameObject attackTowerPrefab;
    public GameObject healingTowerPrefab;
    public GameObject boostTowerPrefab;

    public LayerMask tileLayer; // Layer mask for detecting tiles

    GameObject towerToPlace = null;
    bool canPlaceTower = false;

    void Update()
    {
        // Check if the player has clicked on a tile
        if (canPlaceTower && Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits a tile
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayer))
            {
                // Place the tower prefab on the clicked tile position
                if (!hit.transform.parent || !hit.transform.parent.GetComponent<TowerTile>())
                {
                    Debug.Log("Cannot Place Tower and Selected Tile");
                    return;
                }

                if (hit.transform.parent.GetComponent<TowerTile>().hasTower)
                {
                    Debug.Log("Selected Tile already has Tower");
                    return;
                }

                hit.transform.parent.GetComponent<TowerTile>().PlaceTower(towerToPlace);

                canPlaceTower = false;
                towerToPlace = null;
            }
        }
    }

    public void PrepareAttackTower()
    {
        towerToPlace = attackTowerPrefab;

        canPlaceTower = true;
    }

    public void PrepareHealingTower()
    {
        towerToPlace = healingTowerPrefab;

        canPlaceTower = true;
    }

    public void PrepareBoostTower()
    {
        towerToPlace = boostTowerPrefab;

        canPlaceTower = true;
    }
}

