using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public GameObject[,] grid;

    [Header("Grid Size (cells)")]
    public int width = 11;
    public int height = 9;

    [Header("Cell")]
    [Min(0.01f)]
    public float cellSize = 1f;

    [Header("Gizmos")]
    public Color gizmoColor = new Color(0f, 1f, 1f, 0.6f);

    public GameObject pinGameObject;

    [Header("Pin Layout")]
    public bool enableRowOffset;
    [Tooltip("Applied to odd rows (row 2,4,6...) counting from the top).")]
    public float oddRowXOffset = 0.5f;

    public GameObject ScorePitHigh;
    public GameObject ScorePitMid;
    public GameObject ScorePitMidLow;
    public GameObject ScorePitLow;
    
    // Start is called before the first frame update
    void Start()
    {
        grid = new GameObject[width, height];
        BuildPins();
        BuildScorePitsBottomRow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BuildPins()
    {
        if (pinGameObject == null) return;
        if (width <= 0 || height <= 0) return;

        int centerX = width / 2; // width=11 -> 5 (0-based), i.e. "第6格"

        // Pattern "row 1/2/3..." starts from the last row in the grid (top visually),
        // so rowIndex=0 maps to y=height-1.
        for (int rowIndex = 0; rowIndex < height; rowIndex++)
        {
            int y = (height - 1) - rowIndex;
            if (y <= 1) continue; // bottom rows are reserved (score pits + empty/other)

            // Staggered pattern (like: 000X000 / 00X0X00 / 0X0X0X0):
            // row0: center
            // row1: center-1, center+1
            // row2: center-2, center, center+2
            // ... i.e. step=2 positions, parity alternates per rowIndex.
            int halfSpan = rowIndex;
            int startX = Mathf.Max(0, centerX - halfSpan);
            int endX = Mathf.Min(width - 1, centerX + halfSpan);

            float rowXOffsetWorld = 0f;
            if (enableRowOffset && (rowIndex % 2 == 1))
            {
                rowXOffsetWorld = oddRowXOffset * cellSize;
            }

            for (int x = startX; x <= endX; x++)
            {
                int dx = x - centerX;
                if ((Mathf.Abs(dx) % 2) != (rowIndex % 2)) continue;

                if (grid[x, y] != null) continue;

                Vector3 pos = GetCellCenterWorld(x, y) + new Vector3(rowXOffsetWorld, 0f, 0f);
                GameObject pin = Instantiate(pinGameObject, pos, pinGameObject.transform.rotation, transform);
                grid[x, y] = pin;
            }
        }
    }

    private void BuildScorePitsBottomRow()
    {
        if (width <= 0 || height <= 0) return;

        int y = 0; // bottom row
        int centerX = width / 2;

        PlacePit(centerX, y, ScorePitHigh);
        PlacePit(centerX - 1, y, ScorePitMid);
        PlacePit(centerX + 1, y, ScorePitMid);
        PlacePit(centerX - 2, y, ScorePitMidLow);
        PlacePit(centerX + 2, y, ScorePitMidLow);
        PlacePit(centerX - 3, y, ScorePitMidLow);
        PlacePit(centerX + 3, y, ScorePitMidLow);
        PlacePit(centerX - 4, y, ScorePitLow);
        PlacePit(centerX + 4, y, ScorePitLow);
        PlacePit(centerX - 5, y, ScorePitLow);
        PlacePit(centerX + 5, y, ScorePitLow);
    }

    private void PlacePit(int x, int y, GameObject prefab)
    {
        if (prefab == null) return;
        if (x < 0 || x >= width || y < 0 || y >= height) return;
        if (grid[x, y] != null)
        {
            // If something already occupies this cell (e.g. from a previous run),
            // replace it only if it is owned by this GridSystem.
            if (grid[x, y].transform.parent == transform)
            {
                Destroy(grid[x, y]);
            }
            else
            {
                return;
            }
        }

        Vector3 pos = GetCellCenterWorld(x, y);
        GameObject pit = Instantiate(prefab, pos, prefab.transform.rotation, transform);
        grid[x, y] = pit;
    }

    public Vector3 GetCellCenterWorld(int x, int y)
    {
        Vector3 origin = GetGridOriginWorld();
        return origin + new Vector3(x * cellSize, y * cellSize, 0f);
    }

    /// <summary>Maps a world point to the nearest cell index. Returns false if outside the grid.</summary>
    public bool TryWorldToCell(Vector3 worldPosition, out int x, out int y)
    {
        Vector3 origin = GetGridOriginWorld();
        x = Mathf.RoundToInt((worldPosition.x - origin.x) / cellSize);
        y = Mathf.RoundToInt((worldPosition.y - origin.y) / cellSize);
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    /// <summary>Spawns <paramref name="prefab"/> at cell (x,y), destroying any existing occupant.</summary>
    public void PlaceOrReplaceInCell(int x, int y, GameObject prefab)
    {
        if (prefab == null) return;
        if (x < 0 || x >= width || y < 0 || y >= height) return;

        if (grid[x, y] != null)
        {
            Destroy(grid[x, y]);
            grid[x, y] = null;
        }

        Vector3 pos = GetCellCenterWorld(x, y);
        GameObject go = Instantiate(prefab, pos, prefab.transform.rotation, transform);
        grid[x, y] = go;
    }

    public Vector3 GetGridOriginWorld()
    {
        float halfWidth = (width - 1) * 0.5f * cellSize;
        float halfHeight = (height - 1) * 0.5f * cellSize;
        return transform.position - new Vector3(halfWidth, halfHeight, 0f);
    }

    private void OnDrawGizmos()
    {
        if (width <= 0 || height <= 0 || cellSize <= 0f) return;

        Gizmos.color = gizmoColor;
        Vector3 origin = GetGridOriginWorld();
        Vector3 size = new Vector3(cellSize, cellSize, 0f);
        Vector3 half = size * 0.5f;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 center = origin + new Vector3(x * cellSize, y * cellSize, 0f);
                Gizmos.DrawWireCube(center, size);
            }
        }

        // Outline (optional but helps readability)
        Vector3 bottomLeft = origin - half;
        Vector3 topRight = origin + new Vector3((width - 1) * cellSize, (height - 1) * cellSize, 0f) + half;
        Vector3 bottomRight = new Vector3(topRight.x, bottomLeft.y, bottomLeft.z);
        Vector3 topLeft = new Vector3(bottomLeft.x, topRight.y, bottomLeft.z);
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
    }
}
