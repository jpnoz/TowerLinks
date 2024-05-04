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
                if (!selectedTower)
                {
                    selectedTower = hit.transform.gameObject;
                }
                else if (hit.transform.gameObject.GetComponent<TowerData>())
                {
                    Debug.Log($"Connected {selectedTower.name} to {hit.transform.name}");
                    hit.transform.gameObject.GetComponent<TowerData>().connectedTowers.Add(selectedTower);
                }
            }
        }
    }
}
