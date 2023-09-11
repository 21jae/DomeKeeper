using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class mapgenerator123 : MonoBehaviour
{
    public int width, height;

    public string seed;
    public bool useRandomSeed;

    [Range(0, 100)]
    public int randomFillPercent;
    int[,] map;

    [SerializeField] Tilemap tilemap;
    [SerializeField] Tile Ground;
    [SerializeField] Tile mineral;
    [SerializeField] TileBase ruleTile;

    private void Start()
    {
        GenerateMap();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GenerateMap();
        }
    }
    void RandomFillMap()
    {
        if (useRandomSeed)
        {
            seed = Time.time.ToString();
        }

        System.Random pseudoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = pseudoRandom.Next(0, 100) < randomFillPercent ? 1 : 0;
                }
            }
        }
    }

    void GenerateMap()
    {
        map = new int[width, height];
        RandomFillMap();
        SetTile();

        for (int i = 0; i < 5; i++)
        {
            SmoothMap();
        }
    }

    void SetTile()
    {
        if (map is null)
        {
            return;
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int pos = new Vector3Int(-width / 2 + x, -height / 2 + y, 0);

                if (map[x, y] == 1)
                {
                    tilemap.SetTile(pos,ruleTile);
                }
                else if (map[x, y] == 0)
                {
                    tilemap.SetTile(pos, null);
                }
            }
        }
    }

    int GetAdjustCells(int currentX, int currentY)
    {
        int cells = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) { continue; }
                int adjX = currentX + i;
                int adjY = currentY + j;
                if (adjX < 0 || adjY < 0 || adjX >= width || adjY >= height) ++cells;
                else cells += map[adjX, adjY];
            }
        }
        return cells;
    }

    void SmoothMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetAdjustCells(x, y);
                if (neighbourWallTiles > 4)
                {
                    map[x, y] = 1;
                }
                else if (neighbourWallTiles < 4)
                {
                    map[x, y] = 0;
                }
            }
        }
    }

}
