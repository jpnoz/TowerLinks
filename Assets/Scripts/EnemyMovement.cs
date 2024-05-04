using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    public float movementElevation = 0.0f;

    bool hasReachedGoal = false;
    float tileMovePercent = 0.0f;
    public GameObject currentTile;
    public GameObject nextTile;

    // Start is called before the first frame update
    void Start()
    {
        GameObject startTile = GetCurrentTile();
        if (startTile != null)
        {
            currentTile = startTile;
            nextTile = GetNextTile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasReachedGoal)
        {
            Move();
        }
    }

    private void Move()
    {
        tileMovePercent += movementSpeed * Time.deltaTime;

        if (tileMovePercent >= 1.0f)
        {
            currentTile = nextTile;
            nextTile = GetNextTile();
            tileMovePercent = 0;

            transform.rotation = currentTile.transform.GetChild(0).rotation;
        }

        if (!nextTile)
        {
            hasReachedGoal = true;
            return;
        }

        transform.position = Vector3.LerpUnclamped(currentTile.transform.position, nextTile.transform.position, tileMovePercent) + Vector3.up * (0.5f + movementElevation);
    }

    GameObject GetCurrentTile()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -(1 + movementElevation) * transform.up, out hit))
        {
            TileData currentTileData = hit.transform.GetComponentInParent<TileData>();

            if (currentTileData) 
            {
                return hit.transform.parent.parent.gameObject;
            }
        }

        return null;
    }

    GameObject GetNextTile()
    {
        if (!currentTile)
        {
            return null;
        }

        GameObject nextTile = currentTile.GetComponent<TileData>().nextTile;

        if (!nextTile || !nextTile.GetComponent<TileData>())
        {
            return null;
        }

        return nextTile;
    }
}
