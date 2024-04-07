using System.Collections;
using System.Collections.Generic;
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
    SerializedProperty baseTileMaterial;
    SerializedProperty enemyTileMaterial;
    SerializedProperty spawnTileMaterial;
    SerializedProperty goalTileMaterial;
    SerializedProperty baseWalkDirectionMaterial;
    SerializedProperty spawnWalkDirectionMaterial;

    private void OnEnable()
    {
        tileType = serializedObject.FindProperty("tileType");
        tileMoveDirection = serializedObject.FindProperty("tileMoveDirection");
        baseTileMaterial = serializedObject.FindProperty("baseTileMaterial");
        enemyTileMaterial = serializedObject.FindProperty("enemyTileMaterial");
        spawnTileMaterial = serializedObject.FindProperty("spawnTileMaterial");
        goalTileMaterial = serializedObject.FindProperty("goalTileMaterial");
        baseWalkDirectionMaterial = serializedObject.FindProperty("baseWalkDirectionMaterial");
        spawnWalkDirectionMaterial = serializedObject.FindProperty("spawnWalkDirectionMaterial");
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

        EditorGUILayout.PropertyField(baseTileMaterial);
        EditorGUILayout.PropertyField(enemyTileMaterial);
        EditorGUILayout.PropertyField(spawnTileMaterial);
        EditorGUILayout.PropertyField(goalTileMaterial);
        EditorGUILayout.PropertyField(baseWalkDirectionMaterial);
        EditorGUILayout.PropertyField(spawnWalkDirectionMaterial);

        serializedObject.ApplyModifiedProperties();
    }
}

[ExecuteInEditMode]
public class TileData : MonoBehaviour
{
    public TileType tileType;
    public TileMoveDirection tileMoveDirection;

    [SerializeField] Material baseTileMaterial;
    [SerializeField] Material enemyTileMaterial;
    [SerializeField] Material spawnTileMaterial;
    [SerializeField] Material goalTileMaterial;
    [SerializeField] Material baseWalkDirectionMaterial;
    [SerializeField] Material spawnWalkDirectionMaterial;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!EditorApplication.isPlaying)
        {
            DisplayTileData();
        }
    }

    void DisplayTileData()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        
        if (renderer == null)
        {
            return;
        }

        switch (tileType)
        {
            case TileType.EnemySpawn:
                renderer.sharedMaterial = spawnWalkDirectionMaterial;
                break;
            case TileType.EnemyWalkable:
                renderer.sharedMaterial = baseWalkDirectionMaterial;
                break;
            case TileType.EnemyGoal:
                renderer.sharedMaterial = goalTileMaterial;
                break;
            default:
                renderer.sharedMaterial = baseTileMaterial;
                break;
        }

        if (tileType == TileType.EnemySpawn || tileType == TileType.EnemyWalkable)
        {
            switch (tileMoveDirection)
            {
                case TileMoveDirection.PositiveZ:
                    transform.rotation = Quaternion.identity;
                    break;
                case TileMoveDirection.PositiveX:
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    break;
                case TileMoveDirection.NegativeZ:
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    break;
                case TileMoveDirection.NegativeX:
                    transform.rotation = Quaternion.Euler(0, -90, 0);
                    break;
            }
        }
    }
}
