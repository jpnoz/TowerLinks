using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject selectedTower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.gameObject.GetComponent<TowerData>())
                {
                    GameObject hitObject = hit.transform.gameObject;
                    TowerData hitTowerData = hitObject.GetComponent<TowerData>();

                    if (hitTowerData.connectedTowers.Count >= hitTowerData.maxConnections)
                    {
                        Debug.Log($"{hitObject.name} has reached max connections");
                        return;
                    }

                    if (!selectedTower)
                    {
                        selectedTower = hitObject;
                    }
                    else
                    {
                        TowerData selectedTowerData = selectedTower.GetComponent<TowerData>();

                        if (selectedTower == hitObject)
                        {
                            Debug.Log("Can't connect tower to self");
                            return;
                        }

                        if (selectedTowerData.connectedTowers.Count >= selectedTowerData.maxConnections)
                        {
                            Debug.Log($"{selectedTower.name} has reached max connections");
                            return;
                        }

                        Debug.Log($"Connected {selectedTower.name} and {hit.transform.name}");

                        selectedTowerData.ConnectTower(hitTowerData);
                        hitTowerData.ConnectTower(selectedTowerData);

                        selectedTower = null;
                    }
                }
                
            }
        }
    }
}
