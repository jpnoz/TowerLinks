using UnityEngine;
using UnityEngine.EventSystems;

public class DamageTowerButton : MonoBehaviour
{
    public GameObject damageTowerPrefab; // The damage tower prefab you want to place
    public LayerMask tileLayer; // Layer mask for detecting tiles

    private void Update()
    {
        // Check if the player has clicked on a tile
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits a tile
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayer))
            {
                // Place the tower prefab on the clicked tile position
                Instantiate(damageTowerPrefab, hit.point, Quaternion.identity);
            }
        }
    }
}

