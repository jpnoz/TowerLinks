using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

    BoardGenerator boardGenerator;
    bool wasModified = false;

    void Awake()
    {
        boardGenerator = GetComponentInParent<BoardGenerator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
            DisplayTileDirection();
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

    void DisplayTileDirection()
    {
        if (transform.childCount == 0)
        {
            return;
        }

        
    }
}
