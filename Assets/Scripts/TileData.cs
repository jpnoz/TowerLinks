using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public enum TileMoveDirection
{
    PositiveZ,
    PositiveX,
    NegativeZ,
    NegativeX
}

public enum TileType
{
    TurretPlaceable,
    EnemySpawn,
    EnemyWalkable,
    EnemyGoal
}

[CustomEditor(typeof(TileData))]
[CanEditMultipleObjects]
public class TileDataEditor : Editor
{
    SerializedProperty tileType;
    SerializedProperty tileMoveDirection;

    private void OnEnable()
    {
        tileType = serializedObject.FindProperty("tileType");
        tileMoveDirection = serializedObject.FindProperty("tileMoveDirection");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(tileType);

        // if (tileType == (EnemySpawn || EnemyWalkable))
        if (tileType.enumValueIndex == 1 || tileType.enumValueIndex == 2)
        {
            EditorGUILayout.PropertyField(tileMoveDirection);
        }

        serializedObject.ApplyModifiedProperties();
    }
}

[SelectionBase]
[ExecuteInEditMode]
public class TileData : MonoBehaviour
{
    public Vector2Int tilePosition = Vector2Int.zero;
    public TileType tileType;
    public TileMoveDirection tileMoveDirection;
    public GameObject nextTile = null;

    BoardGenerator boardGenerator;
    bool wasModified = false;

    void Awake()
    {
        boardGenerator = GetComponentInParent<BoardGenerator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // If enemies can walk on the tile,
        // get the next tile in the path through a raycast
        if (tileType == TileType.EnemySpawn || tileType == TileType.EnemyWalkable)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.GetChild(0).forward * (1 + boardGenerator.boardSpacing), out hit))
            {
                nextTile = hit.transform.parent.parent.gameObject;
            }
        }
    }

    private void OnValidate()
    {
        wasModified = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (wasModified)
        {
            DisplayTile();
            UpdateTileScripts();
            wasModified = false;
        }
    }

    void DisplayTile()
    {
        if (!boardGenerator)
        {
            return; 
        }

        // Set child Prefab to match currently set Tile Type
        if (transform.childCount > 0)
        {
            BoardCleanup.DestroyOnUpdate.Add(transform.GetChild(0).gameObject);
        }

        GameObject newTilePrefab = boardGenerator.baseTilePrefab;
        switch (tileType)
        {
            case TileType.EnemySpawn:
                newTilePrefab = boardGenerator.enemySpawnTilePrefab;
                break;
            case TileType.EnemyWalkable:
                newTilePrefab = boardGenerator.enemyWalkTilePrefab;
                break;
            case TileType.EnemyGoal:
                newTilePrefab = boardGenerator.enemyGoalTilePrefab;
                break;
        }

        Quaternion newTileRotation = Quaternion.identity;
        if (tileType == TileType.EnemySpawn || tileType == TileType.EnemyWalkable)
        {
            switch (tileMoveDirection)
            {
                case TileMoveDirection.PositiveZ:
                    newTileRotation = Quaternion.identity;
                    break;
                case TileMoveDirection.PositiveX:
                    newTileRotation = Quaternion.Euler(0, 90, 0);
                    break;
                case TileMoveDirection.NegativeZ:
                    newTileRotation = Quaternion.Euler(0, 180, 0);
                    break;
                case TileMoveDirection.NegativeX:
                    newTileRotation = Quaternion.Euler(0, -90, 0);
                    break;
            }
        }
        GameObject.Instantiate(newTilePrefab, transform.position, newTileRotation, transform);
    }

    void UpdateTileScripts()
    {
        switch (tileType)
        {
            case TileType.TurretPlaceable:
                ClearTileScripts();
                break;
            case TileType.EnemySpawn:
                if (!GetComponent<EnemySpawner>())
                {
                    ClearTileScripts();
                    gameObject.AddComponent<EnemySpawner>();
                }
                break;
            case TileType.EnemyWalkable:
                ClearTileScripts();
                break;
            case TileType.EnemyGoal:
                ClearTileScripts();
                break;
        }
    }

    void ClearTileScripts()
    {
        EnemySpawner enemySpawner = GetComponent<EnemySpawner>();

        if (enemySpawner)
        {
            Destroy(enemySpawner);
        }
    }
}
