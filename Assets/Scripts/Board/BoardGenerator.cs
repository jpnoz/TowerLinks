using UnityEngine;

[ExecuteInEditMode]
public class BoardGenerator : MonoBehaviour
{
    public int boardWidth = 20;
    public int boardHeight = 20;
    public float boardSpacing = 0.1f;

    public Transform cameraPivot;

    // Controls how the board expands and contracts when changing sizes
    // If true, board will expand around center instead of around the origin 
    [SerializeField] bool centerExpand = false;

    [SerializeField] GameObject editableTilePrefab;

    // Prefabs for each Tile Type
    public GameObject baseTilePrefab;
    public GameObject enemySpawnTilePrefab;
    public GameObject enemyWalkTilePrefab;
    public GameObject enemyGoalTilePrefab;

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
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject targetTile = transform.GetChild(i).gameObject;
                TileData targetTileData = targetTile.GetComponent<TileData>();
                if (board[targetTileData.tilePosition.y, targetTileData.tilePosition.x] == null)
                {
                    board[targetTileData.tilePosition.y, targetTileData.tilePosition.x] = targetTile;
                }
            }
        }
        else
        {
            board = CenterExistingBoard(board, boardWidth, boardHeight);
        }

        // Fill remaining spots with new Tiles
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

        // Adjust Camera Pivot to new center of board
        float pivotX = (1 + boardSpacing) * ((boardWidth / 2) - 0.5f);
        float pivotZ = (1 + boardSpacing) * ((boardHeight / 2) - 0.5f);
        cameraPivot.position = new Vector3(pivotX, 0, pivotZ);
    }

    GameObject InstantiateNewTile(int x, int y)
    {
        GameObject newTile = GameObject.Instantiate(editableTilePrefab, new Vector3(x, 0, y) * (1 + boardSpacing), Quaternion.identity, transform);
        newTile.GetComponent<TileData>().tilePosition = new Vector2Int(x, y);
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
                    newBoard[y, x].GetComponent<TileData>().tilePosition = new Vector2Int(x, y);
                    newBoard[y, x].transform.position = new Vector3(x, 0, y) * (1 + boardSpacing);
                }
            }
        }

        return newBoard;
    }
}