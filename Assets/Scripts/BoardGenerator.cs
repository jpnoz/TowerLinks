using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class BoardGenerator : MonoBehaviour
{
    [SerializeField] int boardWidth = 20;
    [SerializeField] int boardHeight = 20;
    // Controls how the board expands and contracts when changing sizes
    // If true, board will expand around center instead of around the origin 
    [SerializeField] bool centerExpand = false;
    [SerializeField] GameObject tilePrefab;
    GameObject[,] board;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (board == null || boardWidth != board.GetLength(1) || boardHeight != board.GetLength(0))
        {
            GenerateBoard();
        }
    }

    void GenerateBoard()
    {
        if (board == null)
        {
            board = new GameObject[boardHeight, boardWidth];

            // Reattach any remaining children
            for (int y = 0; y < boardHeight; y++)
            {
                for (int x = 0; x < boardWidth; x++)
                {
                    GameObject targetTile = GameObject.Find($"({x}, {y})");
                    if (targetTile != null && board[y, x] == null)
                    {
                        board[y, x] = targetTile;
                        Debug.Log($"({x}, {y}) reattached");
                    }
                }
            }
        }
        else
        {
            board = CenterExistingBoard(board, boardWidth, boardHeight);
        }

        for (int y = 0; y < board.GetLength(0); y++)
        {
            for (int x = 0; x < board.GetLength(1); x++)
            {
                if (board[y, x] == null)
                {
                    board[y, x] = InstantiateNewTile(x, y);
                }
            }
        }
    }

    GameObject InstantiateNewTile(int x, int y)
    {
        GameObject newTile = GameObject.Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.identity, transform);
        newTile.name = $"({x}, {y})";

        return newTile;
    }

    GameObject[,] CenterExistingBoard(GameObject[,] oldBoard, int newWidth, int newHeight)
    {
        int oldWidth = oldBoard.GetLength(1);
        int oldHeight = oldBoard.GetLength(0);
        Vector2Int oldCenter = new Vector2Int(oldWidth / 2, oldHeight / 2);
        Vector2Int newCenter = new Vector2Int(newWidth / 2, newHeight / 2);

        Vector2Int minBoardSize = new Vector2Int(Mathf.Min(oldWidth, newWidth), Mathf.Min(oldHeight, newHeight));
        Vector2Int halfMinBoardSize = minBoardSize / 2;

        // Center Board around origin if not set to expand around center
        if (!centerExpand)
        {
            oldCenter = Vector2Int.zero;
            newCenter = Vector2Int.zero;
            halfMinBoardSize = Vector2Int.zero;
        }

        GameObject[,] newBoard = new GameObject[newHeight, newWidth];

        // Transfer all tiles that fit in new array from old array
        for (int y = 0; y < minBoardSize.y; y++)
        {
            for (int x = 0; x < minBoardSize.x; x++)
            {
                newBoard[newCenter.y - halfMinBoardSize.y + y, newCenter.x - halfMinBoardSize.x + x] = oldBoard[oldCenter.y - halfMinBoardSize.y + y, oldCenter.x - halfMinBoardSize.x + x];
                oldBoard[oldCenter.y - halfMinBoardSize.y + y, oldCenter.x - halfMinBoardSize.x + x] = null;
            }
        }

        // Mark tiles that don't fit in new board to be destroyed
        for (int y = 0; y < oldHeight; y++)
        {
            for (int x = 0; x < oldWidth; x++)
            {
                if (oldBoard[y, x] != null)
                {
                    BoardCleanup.DestroyOnUpdate.Add(oldBoard[y, x]);
                }
            }
        }

        // Reposition Tiles in new Board
        for (int y = 0; y < newHeight; y++)
        {
            for (int x = 0; x < newWidth; x++)
            {
                if (newBoard[y, x] != null)
                {
                    newBoard[y, x].name = $"({x}, {y})";
                    newBoard[y, x].transform.position = new Vector3(x, 0, y);
                }
            }
        }

        return newBoard;
    }
}
